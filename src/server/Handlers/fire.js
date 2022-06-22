module.exports = {
   type: "fire",
   handle: (ws, data) => {
      if (ws.match == null) return;

      const payload = JSON.parse(data);

      ws.match.fire(ws, payload.angle, payload.firedPos);
   }
}