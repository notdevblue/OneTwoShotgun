class Vector2
{
   constructor(x, y) {
      this.x = x;
      this.y = y;
   }

   add(right) {
      return new Vector2(this.x + right.x,
                         this.y + right.y);
   }

   addself(right) {
      this.x += right.x;
      this.y += right.y;
   }


}

module.exports = Vector2;