const hs = require("../HanSocket/HanSocket.js");
const query = require("../Utils/mysql-extensions.js");
const parse = require("../Utils/ParsePacket.js");

module.exports = {
   type: "sign",
   handle: (ws, data) => {
      if (ws.loggedin) return; // TODO: 잘못된 요청

      const obj = parse(data);
      (obj.signup ? query.signup : query.signin)
         (obj.nickname, obj.password, res => {
            const payload = hs.toJson("loginresult", JSON.stringify({
               success: res
            }));
            hs.send(ws, payload);
         });
      
      
      
   }
}