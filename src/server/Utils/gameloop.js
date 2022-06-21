let activeLoops = [];
const getLoopId = (function () {
   let staticLoopId = 0;
   return function () {
      return staticLoopId++;
   }
})();

module.exports.setGameLoop = function (update, tickLength = 1000 / 30) {
   let lastUpdate = Date.now();
   return activeLoops.push(setInterval(() => {
      let now = Date.now();
      let deltaTime = 1.0 / (now - lastUpdate);
      lastUpdate = now;

      update(deltaTime);
   }, tickLength));
}

module.exports.clearGameLoop = (id) => {
   clearInterval(id);
}

// module.exports.setGameLoop = function (update, tickLength = 1000 / 30) {
//    let loopId = getLoopId();
//    activeLoops.push(loopId);

//    const longWaitMs = Math.floor(tickLength - 1);

//    let prev = process.hrtime();
//    let target = process.hrtime();

//    const gameLoop = function () {
//       const now = process.hrtime();

//       if (now >= target) {
//          const delta = now - prev;
//          prev = now;
//          target = now + tickLength;
//          console.log(now);
//          console.log(target);
//          // console.log(prev);
//          update(delta);
//       }

//       if (activeLoops.indexOf(loopId) === -1) {
//          return;
//       }

//       const remainingTick = target - process.hrtime();
//       if (remainingTick > longWaitMs) {
//          setTimeout(gameLoop, Math.max(longWaitMs, 16));
//       } else {
//          setImmediate(gameLoop);
//       }
//    }

//    gameLoop();

//    return loopId;
// }

// module.exports.clearGameLoop = loopId => {
//    activeLoops.splice(activeLoops.indexOf(loopId), 1);
// }