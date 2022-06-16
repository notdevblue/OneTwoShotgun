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