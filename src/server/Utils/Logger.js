const path = require("path");
const { exit, exitCode } = require("process");

const fs = require("fs").promises;
const dir = path.join(process.env.PWD, ".Log");


fs.access(dir)
   .catch(err => {
      fs.mkdir(dir);
   });

module.exports = (msg) => {
};

console.log(filename());


function filename() {
   var date = new Date();
   return `${date.getUTCFullYear()}:${date.getUTCMonth()}:${date.getUTCDate()}`;
}