const hs = require("../HanSocket/HanSocket.js");
const Vector2 = require("../Utils/Vector2.js");

const speed = 3.0;
const runMul = 1.2;

module.exports = {
   type: "move",
   handle: (ws, data) => {
      const [delta, run] = JSON.parse(data);

      if (run) {
         delta.x *= 1.2;
         delta.y *= 1.2;
      }

      ws.posiiton.addself({ x: delta.x, y: delta.y });

   }
}