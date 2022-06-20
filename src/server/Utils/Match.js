const hs    = require("../HanSocket/HanSocket.js");
const write = require("./Logger.js");

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

      // 입장 이벤트
      const joinedPayload
         = hs.toJson("joined", JSON.stringify({
            id: ws.id, nickname: ws.nickname
         }));
      this.broadcast(joinedPayload);

      write(`[  ] Client ${ws.id} joined match: ${this.id}`, ws.ipAddr);
   }

   leave(ws) {
      this.broadcast(hs.toJson("left", JSON.stringify({ id: ws.id })));

      this.players.splice(this.players.indexOf(ws.id), 1);

      write(`[  ] Client ${ws.id} left match: ${this.id}`, ws.ipAddr);

   }
}

module.exports = new Match();