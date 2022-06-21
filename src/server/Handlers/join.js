const write = require("../Utils/Logger");
const Match = require("../Utils/Match");
const Vector2 = require("../Utils/Vector2.js");

module.exports = {
   type: "join",
   handle: (ws, data) => {
      if (!ws.loggedin) {
         write(`[WW] Wrong request from ${ws.id}, reason: not logged in`, ws.ipAddr);
         return;
      }

      ws.match          = Match.currentQueueMatch();
      ws.position       = new Vector2(0.0, 0.0);
      ws.deltaPosition  = new Vector2(0.0, 0.0);
      ws.match.join(ws);
   }
};