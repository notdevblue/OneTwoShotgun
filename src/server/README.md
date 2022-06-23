## 페킷 정리


### 인증 (클라이언트 -> 서버)

<br/>

닉네임 검색
```js
checknickname: {
   string: nickname
}
```

<br/>

로그인, 가입
```js
sign: {
   string: nickname
   string: password
   bool: signup
}
```

<br/>

### 인증 (서버 -> 클라이언트)

<br/>

회원가입 요청
```js
asksignup: {
}
```

<br/>

로그인 요청
```js
loginrequest: {
}
```

<br/>

로그인 처리 성공 여부
```js
loginresult: {
   bool: success
}
```

<br/>

### 큐 (클라이언트 -> 서버)

```js
join: {
}
```

<br/>

### 큐 (서버 -> 클라이언트)

```js
joined: {
   int: id,
   int: hp,
   string: nickname
}
```

<br/>

```js
left: {
   int: id
}
```

<br/>

```js
roomdata: {
   List<joined> userlist
}
```

<br/>

```js
gamestart: {
}
```

<br/>


### 플레이어 (클라이언트 -> 서버)

```js
move: {
   Vector2: delta,
   bool: run
}
```

<br/>


```js
fire: {
   float: angle
}
```

```js
hit: {
}
```

<br/>

### 플레이어 (서버 -> 클라이언트)

```js
playerinit: {
   int: hp,
   Vector2: pos
}
```

<br/>

```js
moveto: {
   int: id,
   Vector2: target
}
```

<br/>

<!-- ```js
fired: {
   int id,
   Vector2: firedPos,
   List<float>: angles
}
``` -->

```js
bulletdata: {
   List<bullets>: bullets {
      int: id,
      Vector2: pos,
   }
}
```

<br/>

```js
hit: {
   int: id,
   int: hp
}
```

<br/>

```js
dead: {
   int: id
}
```

<br/>

### 인게임 (클라이언트 -> 서버)

```js
loaded: {
}
```

<br/>

### 인게임 (서버 -> 클라이언트)

```js
loadedtimeout: {
   int: id
}
```

<br/>

```js
allloaded: {
}
```

<br/>