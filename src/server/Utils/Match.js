const hs    = require("../HanSocket/HanSocket.js");
const { setGameLoop, clearGameLoop } = require("./gameloop.js");
const write = require("./Logger.js");
const Vector2 = require("./Vector2.js");

class Match
{
   constructor() {
      this.matches = [];
      this.matchid = 0;
   }

   currentQueueMatch() {
      let match = this.matches.find(x => x.queue);

      if (match == undefined) {
         match = this.newMatch();
      }

      return match;
   }

   startMatch() {
      this.matches.find(x => x.queue).start();
   }

   newMatch() {
      if (this.matches.findIndex(x => x.queue) != -1) return;

      write(`[II] New match created. ID: ${this.matchid + 1}`);

      this.matches[this.matchid] = new Game(this.matchid);
      return this.matches[this.matchid++];
   }
}

class Game
{
   constructor(id) {
      this.queue = true;
      this.players = [];
      this.bullets = [];
      this.id = id;
      this.moveloopId = -1;
      this.bulletloopId = -1;

      this.playerSpeed = 8;
      this.playerHp = 100;

      this.shellAngle = 25.0;
      this.shellLifetime = 10000; // ms
      this.shellSpeed = 40.0;
      this.shellDamage = 10;
      this.shellCount = 4;
      this.shellId = 0;
      this.shellCollisionDistance = 0.5;

      this.halfAngle = this.shellAngle / 2.0;
      this.halfCount = this.shellCount / 2.0;

      this.queueTimeMs = 5000;
      this.moveFps = 1000 / 30;
      this.bulletFps = 1000 / 15;
   }

   start() {
      this.queue = false;
      this.broadcast("gamestart", "");
   }

   broadcast(payload) {
      this.players.forEach(e => {
         hs.send(e, payload);
      });
   }

   join(ws) {
      let roomdataPayload;
      let roomdata = [];

      ws.position = new Vector2(0.0, 0.0);
      ws.hp = this.playerHp;
      ws.dead = false;
      
      // 방 정보
      this.players.forEach(e => {
         roomdata.push({
            id: e.id,
            hp: e.hp,
            nickname: e.nickname
         });
      });
      roomdataPayload = JSON.stringify({ userlist: roomdata });
      hs.send(ws, hs.toJson("roomdata", roomdataPayload));
      
      this.players[ws.id] = ws;

      // 입장 이벤트
      const joinedPayload
         = hs.toJson("joined", JSON.stringify({
            id: ws.id,
            hp: ws.hp,
            nickname: ws.nickname
         }));
      this.broadcast(joinedPayload);

      write(`[  ] Client ${ws.id} joined match: ${this.id}`, ws.ipAddr);

      if (this.moveloopId === -1) {
         this.processPosition();
      }
      if (this.bulletloopId === -1) {
         this.processBullet();
      } 
   }

   leave(ws) {
      this.broadcast(hs.toJson("left", JSON.stringify({ id: ws.id })));

      delete this.players[ws.id];

      write(`[II] Client ${ws.id} left match: ${this.id}`, ws.ipAddr);

      if (Object.keys(this.players).length <= 0) {
         write(`[II] Game ${this.id} loop terminated`);
         this.stopProcessingPosition();
         this.stopProcessingBullet();
      }
   }

   processPosition() {
      this.moveloopId = setGameLoop(deltaTime => {
         this.players.forEach(e => {
            if (e.dead) return;

            e.position.x +=
               e.deltaPosition.x * deltaTime * this.playerSpeed;
            e.position.y +=
               e.deltaPosition.y * deltaTime * this.playerSpeed;

            const payload = JSON.stringify({
               id: e.id,
               pos: e.position
            });

            this.players.forEach(ws => {
               ws.send(hs.toJson("moveto", payload));
            });
            
            e.deltaPosition.reset();
         });
      }, this.moveFps);
   }

   stopProcessingPosition() {
      clearGameLoop(this.moveloopId);
      this.moveloopId = -1;
   }

   move(ws, delta) {
      if (ws.dead) return;

      delta.x = Math.sign(delta.x);
      delta.y = Math.sign(delta.y);
      ws.deltaPosition.addself(delta);
   }

   processBullet() {
      this.bulletloopId = setGameLoop(deltaTime => {
         let bullets = [];
         let removeTargetIdx = [];

         this.bullets.forEach(e => {
            let keepgoing = true;

            // 라이프타임 처리
            if (e.firedat + e.lifetime <= Date.now()) {
               this.bullets.splice(this.bullets.indexOf(e), 1);
               return;
            }

            this.players.forEach(p => {
               if (p.id == e.firedby) return;

               let x = Math.abs(e.pos.x - p.position.x);
               let y = Math.abs(e.pos.y - p.position.y);
               
               // 충돌 처리
               if (Math.sqrt(x * x + y * y) <= this.shellCollisionDistance) {
                  keepgoing = false;
                  this.bullets.splice(this.bullets.indexOf(e), 1);
                  this.hit(p);
                  return;
               }
            });

            if (!keepgoing) return;

            e.pos.x += e.delta.x * deltaTime * e.speed;
            e.pos.y += e.delta.y * deltaTime * e.speed;

            bullets.push({
               id: e.id,
               pos: e.pos
            });
         });

         this.players.forEach(ws => {
            ws.send(hs.toJson("bulletdata", JSON.stringify({
               bullets: bullets
            })));
         })
      }, this.bulletFps);
   }

   stopProcessingBullet() {
      clearGameLoop(this.bulletloopId);
      this.bulletloopId = -1;
      this.bullets = [];
   }

   fire(ws, angle, firedPos) {
      if (ws.dead) return;

      for (let i = -this.halfCount; i <= this.halfCount; ++i) {
         // if (i == 0) continue;

         let shell = {
            id: this.shellId++,
            firedby: ws.id,
            firedat: Date.now(),
            lifetime: this.shellLifetime,
            pos: new Vector2(0, 0),
            delta: new Vector2(0, 0),
            speed: this.shellSpeed,
            angle: (angle + (Math.sign(i)
               * ((Math.random() * this.halfAngle)
                  + ((Math.abs(i) == this.halfCount) * this.halfAngle)))) * Math.PI / 180
         };

         shell.delta.x = Math.cos(shell.angle);
         shell.delta.y = Math.sin(shell.angle);
         shell.pos.x = firedPos.x;
         shell.pos.y = firedPos.y;

         this.bullets.push(shell);
      }
   }

   deletebullet(shell) {
      this.bullets.findIndex(x => x == shell);
   }

   hit(ws) {
      if (ws.dead) return;

      ws.hp -= this.shellDamage;

      if (ws.hp <= 0) {
         this.dead(ws);
         return;
      }

      const payload = JSON.stringify({
         id: ws.id,
         hp: ws.hp
      });

      this.broadcast(hs.toJson("hit", payload));
   }

   dead(ws) {
      ws.dead = true;
      const payload = JSON.stringify({
         id: ws.id
      });
      
      this.broadcast(hs.toJson("dead", payload));
   }

   gamestart() {
      this.broadcast(hs.toJson("gamestart"));
   }
}

module.exports = new Match();