const query = require("../Utils/mysql-extensions.js");
const parse = require("../Utils/ParsePacket.js");
const hs    = require("../HanSocket/HanSocket.js");

module.exports = {
   type: "checknickname",
   handle: async (ws, data) => {
      if (ws.loggedin) return; // TODO: 잘못된 요청

      const obj = parse(data, ws.ipAddr);

      await query.nicknameTaken(obj.nickname, res => {
         hs.send(ws, hs.toJson((res ? "loginrequest" : "asksignup"), ""));
      });
   }
}