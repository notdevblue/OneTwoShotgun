const path = require("path");
const { exit, exitCode } = require("process");

const fs = require("fs").promises;
const dir = path.join(process.env.PWD, ".Log");

// .Log 폴더 확인
fs.access(dir)
   .catch(err => {
      fs.mkdir(dir);
   });

// 로그 헤더
function getHeader() {
   var date = new Date();
   return ` ${date.getHours()}-${date.getMinutes()}-${date.getSeconds()}-${date.getMilliseconds()} `;
}
function getDate() {
   var date = new Date();
   return `${date.getFullYear()}-${date.getMonth()}-${date.getDate()}`;
}

// 로그 작성
function write(msg) {
   var filePath = path.join(dir, getDate());
   var header = getDate() + getHeader();

   fs.access(filePath)
      .catch(err => {
         fs.writeFile(), "", err => {
            console.log(`[II] Created new log file: ${filePath}`);
         }
      });
   
   msg = header + msg;
   
   console.log(msg);
   fs.appendFile(filePath, [msg + "\r\n"]);
}

module.exports = write;