const mysql = require("./mysql");
const { write } = require("./Logger.js");

mysql.init().then(() => {
   write("MySQL init finished.");
});

module.exports = {
   async signup(nickname, pwd) {
      await mysql.query("INSERT INTO `Account`(`nickname`, `pwd`) "
         + "VALUES (?, md5(?));", [nickname, pwd]).then(() => {;
            write(`New account: ${nickname}`);
         });
   },

   async signin(nickname, pwd) {
      await mysql.query("SELECT count(id) FROM `Account` "
         + "WHERE 'nickname'=?, `pwd`=md5(?)", [nickname, pwd]);
   },

   nicknameTaken(nickname) {
      // TODO: 닉네임 확인
   }
};