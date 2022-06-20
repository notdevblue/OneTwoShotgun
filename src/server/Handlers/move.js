const hs = require("../HanSocket/HanSocket.js");
const Vector2 = require("../Utils/Vector2.js");

module.exports = {
   type: "move",
   handle: (ws, data) => {
      if (ws.match == null) return;

      let payload = JSON.parse(data);
      ws.match.move(ws, payload.delta);

   }
}