const mysql = require("./mysql");
const write = require("./Logger.js");

mysql.init().then(() => {
   write("[II] MySQL init finished.");
});

async function signup(nickname, pwd, callback) {
   await mysql.query("INSERT INTO `Account`(`nickname`, `pwd`) "
      + "VALUES (?, md5(?));", [nickname, pwd]).then(res => {
         write(`[II] ${nickname}: New account.`);
         callback(res.affectedRows == 1);
      });
}

async function signin(nickname, pwd, callback) {
   await mysql.query("SELECT count(id) AS cnt FROM `Account` "
      + "WHERE `nickname`=? AND `pwd`=md5(?)", [nickname, pwd]).then(res => {
         write(`[II] ${nickname}: Signed in.`);
         callback(res[0]["cnt"] == 1);
      });
}

async function nicknameTaken(nickname, callback) {
   await mysql.query("SELECT count(id) AS cnt FROM `Account` "
      + "WHERE `nickname`=?", [nickname]).then(res => {
         callback(res[0]["cnt"] == 1);
      });
}

async function updateKills(nickname, kills, callback) {
   await mysql.query("UPDATE `Account` SET `kills`=`kills`+? "
      + "WHERE `nickname`=?", [kills, nickname]).then(res => {
         write(`[II] ${nickname}: Added ${kills} kills.`);
         callback(res.affectedRows == 1);
      });
}

async function updateDeaths(nickname, deaths, callback) {
   await mysql.query("UPDATE `Account` SET `deaths`=`deaths`+? "
      + "WHERE `nickname`=?", [deaths, nickname]).then(res => {
         write(`[II] ${nickname}: Added ${deaths} deaths.`);
         callback(res.affectedRows == 1);
      });
}

async function updateWon(nickname, won, callback) {
   await mysql.query("UPDATE `Account` SET `won`=`won`+? "
      + "WHERE `nickname`=?", [won, nickname]).then(res => {
         write(`[II] ${nickname}: Added ${won} won.`);
         callback(res.affectedRows == 1);
      });
}

async function updatLost(nickname, lost, callback) {
   await mysql.query("UPDATE `Account` SET `lost`=`lost`+? "
      + "WHERE `nickname`=?", [lost, nickname]).then(res => {
         write(`[II] ${nickname}: Added ${lost} lost.`);
         callback(res.affectedRows == 1);
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