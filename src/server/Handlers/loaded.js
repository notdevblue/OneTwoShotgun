module.exports = {
   type: "loaded",
   handle: (ws, data) => {
      if (ws.match == null
         || !ws.match.queue) return;
      
      ws.match.loaded(ws);
   }
}