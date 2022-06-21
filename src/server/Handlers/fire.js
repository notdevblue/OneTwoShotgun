module.exports = {
   type: "fire",
   handle: (ws, data) => {
      if (ws.match == null) return;

      ws.match.fire(ws, JSON.parse(data).angle);
   }
}