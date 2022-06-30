# 사용법

## 서버

* 모듈 설치
```bash
npm install
```

<br/>

* 필요한 헨들러 추가
```js
module.exports = {
   type: "페킷타입",
   handle: (ws, data) => {
      // ws: 페킷 보낸 클라이언트 소켓
      // data: DataVO 중 payload
   }
};
```

<br/>

* 윈도우인 경우 .env 에 PWD 추가 
```bash
...
PWD=이\서버까지의\경로
# 예: C:\Users\Han\Downloads\server
# index.js 파일을 포함하고 있는 디렉토리 까지
...
```


<br/>

* 실행
```bash
node index.js
```
또는
```bash
nodemon
```

* * *

### DataVO
```js
{
  type: "페킷 타입",
  payload: "페킷"
}
```

* * *


## 클라이언트
* HanSocket 폴더 추가
* WebSocketClient.cs 를 씬에 추가
* 필요에 따라 헨들러 추가
```cs
class MyHandler : HandlerBase
{
  protected override string Type => "hit";
  
  protected override void OnArrived(string payload)
  {
     // 페킷 도착했을 때 웹소켓 쓰레드에서 호출됨
     // payload: DataVO 중 payload
  }

  protected override void OnFlag()
  {
     // 페킷 도착하고 OnArrived 후에 유니티 쓰레드에서 호출됨
  }
}
```
