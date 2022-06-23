const hs    = require("../HanSocket/HanSocket.js");
const { setGameLoop, clearGameLoop } = require("./gameloop.js");
const write = require("./Logger.js");
const Vector2 = require("./Vector2.js");

// let ss = [];
// ss["an"] = {a:1};
// ss["a2"] = {b:2};
// ss["aff"] = {c:3};

// console.log(ss);
// delete (ss["an"]);

// // let idx = ss.indexOf(null);
// // console.log(idx);

// // ss.splice(idx, 1);

// console.log(ss);

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
      this.shellLifetime = 10.0;
      this.shellSpeed = 10.0;
      this.shellDamage = 10;
      this.shellCount = 4;
      this.shellId = 0;

      this.halfAngle = this.shellAngle / 2.0;
      this.halfCount = this.shellCount / 2.0;

      this.queueTimeMs = 5000;
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

      ws.position = new Vector2(0.0, 0.0);
      ws.hp = this.playerHp;
      ws.dead = false;

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
   }

   leave(ws) {
      this.broadcast(hs.toJson("left", JSON.stringify({ id: ws.id })));

      delete this.players[ws.id];

      write(`[II] Client ${ws.id} left match: ${this.id}`, ws.ipAddr);

      if (Object.keys(this.players).length <= 0) {
         write(`[II] Game ${this.id} loop terminated`);
         this.stopProcessingPosition();
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
      }, 1000 / 30);
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
      bulletloopId = setGameLoop(deltaTime => {
         let bullets = [];

         this.bullets.forEach(e => {
            e.pos.x += delta.x * deltaTime * e.speed;
            e.pos.y += delta.y * deltaTime * e.speed;

            bullets.push({
               id: e.id,
               pos: e.pos
            });
         });

         this.players.forEach(ws => {
            ws.send(hs.toJson("bulletdata", JSON.stringify({
               bullets: JSON.stringify(bullets)
            })));
         })
      }, 1000 / 20);
   }

   stopProcessingBullet() {

   }

   fire(ws, angle, firedPos) {
      if (ws.dead) return;

      let angles = [];

      for (let i = -this.halfCount; i <= this.halfCount; ++i) {
         if (i == 0) continue;
         const pos = JSON.parse(firedPos);

         let shell = {
            id: shellId++,
            firedby: ws.id,
            pos: new Vector2(0, 0),
            delta: new Vector2(0, 0),
            speed: this.shellSpeed,
            angle: angle + (Math.sign(i)
               * ((Math.random() * this.halfAngle)
                  + ((Math.abs(i) == this.halfCount) * this.halfAngle)))
         };

         shell.delta.x = Math.cos(angle);
         shell.delta.y = Math.sin(angle);
         shell.pos.x = pos.x;
         shell.pos.y = pos.y;

         angles.push(shell.angle);
      }

      // https://math.stackexchange.com/questions/180874/convert-angle-radians-to-a-heading-vector
      // ㄱㅂㅈ 서버사이드 발사 연산

      // const payload = JSON.stringify({
      //    id: ws.id,
      //    firedPos: firedPos,
      //    angles: angles,
      //    speed: this.shellSpeed,
      //    alivefor: this.shellLifetime
      // });
      
      // this.broadcast(hs.toJson("fired", payload));
   }

   deletebullet(shell) {
      this.bullets.findIndex(x => x == shell);
   }

   hit(id) {
      let payload;
      let type;

      let ws = this.players[id];

      if (ws.dead) return;

      ws.hp -= this.shellDamage;

      if (ws.hp <= 0) {
         ws.dead = true;
         payload = JSON.stringify({
            id: id
         });
         type = "dead";
      } else {
         payload = JSON.stringify({
            id: id,
            hp: ws.hp
         });
         type = "hit";
      }

      this.broadcast(hs.toJson(type, payload));
   }

   gamestart() {
      this.broadcast(hs.toJson("gamestart"));
   }
}

module.exports = new Match();