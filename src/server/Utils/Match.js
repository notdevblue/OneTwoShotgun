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
      this.id = id;
      this.loopId = -1;
      this.playerSpeed = 8;
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
         roomdata.push({ id: e.id, nickname: e.nickname });
      });
      roomdataPayload = JSON.stringify({ userlist: roomdata });
      hs.send(ws, hs.toJson("roomdata", roomdataPayload));
      
      this.players[ws.id] = ws;

      ws.position = new Vector2(0.0, 0.0);

      // 입장 이벤트
      const joinedPayload
         = hs.toJson("joined", JSON.stringify({
            id: ws.id, nickname: ws.nickname
         }));
      this.broadcast(joinedPayload);

      write(`[  ] Client ${ws.id} joined match: ${this.id}`, ws.ipAddr);

      if (this.loopId === -1) {
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
      this.loopId = setGameLoop(deltaTime => {
         this.players.forEach(e => {

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
      clearGameLoop(this.loopId);
      this.loopId = -1;
   }

   move(ws, delta) {
      delta.x = Math.sign(delta.x);
      delta.y = Math.sign(delta.y);
      ws.deltaPosition.addself(delta);
   }

   fire(ws, angle) {
      const payload = JSON.stringify({
         id: ws.id,
         angle: angle
      });
      
      this.broadcast(hs.toJson("fired", payload));
   }
}

module.exports = new Match();