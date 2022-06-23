module.exports = {
   type: "hit",
   handle: (ws, data) => {
      if (ws.match == null) return;

      ws.match.hit(ws.id);
   }
}