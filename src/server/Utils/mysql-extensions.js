const mysql = require("./mysql");
const write = require("./Logger.js");

mysql.init().then(() => {
   write("MySQL init finished.");
});

async function signup(nickname, pwd) {
   await mysql.query("INSERT INTO `Account`(`nickname`, `pwd`) "
      + "VALUES (?, md5(?));", [nickname, pwd]).then(e => {
         write(`[II] ${nickname}: New account.`);
         return e.affectedRows == 1;
      });
}

async function signin(nickname, pwd) {
   await mysql.query("SELECT count(id) AS cnt FROM `Account` "
      + "WHERE `nickname`=? AND `pwd`=md5(?)", [nickname, pwd]).then(e => {
         write(`[II] ${nickname}: Signed in.`);
         return res[0]["cnt"] == 1;
      });
}

async function nicknameTaken(nickname) {
   await mysql.query("SELECT count(id) AS cnt FROM `Account` "
      + "WHERE `nickname`=?", [nickname]).then(res => {
         return res[0]["cnt"] == 1;
      });
}

async function updateKills(nickname, kills) {
   await mysql.query("UPDATE `Account` SET `kills`=`kills`+? "
      + "WHERE `nickname`=?", [kills, nickname]).then(res => {
         write(`[II] ${nickname}: Added ${kills} kills.`);
         return res.affectedRows == 1
      });
}

async function updateDeaths(nickname, deaths) {
   await mysql.query("UPDATE `Account` SET `deaths`=`deaths`+? "
      + "WHERE `nickname`=?", [deaths, nickname]).then(res => {
         write(`[II] ${nickname}: Added ${deaths} deaths.`);
         return res.affectedRows == 1
      });
}

async function updatWon(nickname, won) {
   await mysql.query("UPDATE `Account` SET `won`=`won`+? "
      + "WHERE `nickname`=?", [won, nickname]).then(res => {
         write(`[II] ${nickname}: Added ${won} won.`);
         return res.affectedRows == 1
      });
}

async function updatLost(nickname, lost) {
   await mysql.query("UPDATE `Account` SET `lost`=`lost`+? "
      + "WHERE `nickname`=?", [lost, nickname]).then(res => {
         write(`[II] ${nickname}: Added ${lost} lost.`);
         return res.affectedRows == 1
      });
}

module.exports = {
   signup,
   signin,
   nicknameTaken,
   updateKills,
   updateDeaths,
   updatWon,
   updatLost
};