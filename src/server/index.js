const HanSocket = require("./HanSocket/HanSocket.js");
const query = require("./Utils/mysql-extensions.js");

const hs = new HanSocket(process.env.PORT);
hs.process(ws => {
   ws.send();
});

