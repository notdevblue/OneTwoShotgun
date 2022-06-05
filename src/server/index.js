const HanSocket = require("./HanSocket/HanSocket.js");
require("dotenv").config();

const hs = new HanSocket(process.env.PORT);

hs.process();