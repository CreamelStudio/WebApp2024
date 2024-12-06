// 로봇호출 박스 토글 함수 - 로봇 호출 박스를 보여주거나 숨기는 토글 기능을 하는 함수
function toggleCallBox(event) {
	event.preventDefault(); // 기본 이벤트 동작을 막음 (예: 링크 클릭 시 페이지 이동 방지)
	const callBox = document.querySelector('.call-box'); // call-box 클래스를 가진 요소를 찾아서 저장
	callBox.classList.toggle('show'); // show 클래스를 토글(있으면 제거, 없으면 추가)하여 박스를 보여주거나 숨김
}

// 로봇호출 박스 닫기 함수 - 로봇 호출 박스를 닫는(숨기는) 함수
function closeCallBox() {
	const callBox = document.querySelector('.call-box'); // call-box 클래스를 가진 요소를 찾아서 저장
	callBox.classList.remove('show'); // show 클래스를 제거하여 박스를 숨김
}
// 로봇호출 박스 토글 함수 - 로봇 호출 박스를 보여주거나 숨기는 토글 기능을 하는 함수
function toggleCallBox(event) {
	event.preventDefault(); // 기본 이벤트 동작을 막음 (예: 링크 클릭 시 페이지 이동 방지)
	const callBox = document.querySelector('.call-box'); // call-box 클래스를 가진 요소를 찾아서 저장
	callBox.classList.toggle('show'); // show 클래스를 토글(있으면 제거, 없으면 추가)하여 박스를 보여주거나 숨김
}

// 로봇호출 박스 닫기 함수 - 로봇 호출 박스를 닫는(숨기는) 함수
function closeCallBox() {
	const callBox = document.querySelector('.call-box'); // call-box 클래스를 가진 요소를 찾아서 저장
	callBox.classList.remove('show'); // show 클래스를 제거하여 박스를 숨김
}

// 사이드 메뉴 토글 함수 - 사이드 메뉴를 열거나 닫는 토글 기능을 하는 함수
function toggleSideMenu(event) {
	event.preventDefault(); // 기본 이벤트 동작을 막음
	const sideMenu = document.querySelector('.side-menu'); // side-menu 클래스를 가진 요소를 찾아서 저장
	const currentLeft = sideMenu.style.left; // 현재 사이드 메뉴의 left 위치값을 저장
	sideMenu.style.left = currentLeft === '0px' ? '-250px' : '0px'; // 현재 위치가 0px이면 -250px로, 아니면 0px로 설정하여 메뉴를 숨기거나 보여줌
}
if (!navigator.userAgent.includes("YourAppName")) {
    window.location.href = "http://127.0.0.1:5500/main-map/map.html#";
}

const robotCall = document.querySelectorAll('.call-button');

robotCall.forEach(call => {
    const btns = call.parentNode.querySelector('.btns');

    // 버튼 클릭 이벤트
    call.addEventListener('click', () => {
        call.style.display = 'none';
        btns.classList.add('btnsShow');
        console.log('clicked');
    });
});

// 배경 클릭 이벤트
document.addEventListener('click', (event) => {
    robotCall.forEach(call => {
        const btns = call.parentNode.querySelector('.btns');
        // 클릭된 요소가 call-button이나 btns 클래스를 포함하는 요소가 아닐 경우에만 실행
        if (!event.target.closest('.call-button') && !event.target.closest('.btns')) {
            call.style.display = 'block';
            btns.classList.remove('btnsShow');
        }
    });
});

// 사이드 메뉴 토글 함수 - 사이드 메뉴를 열거나 닫는 토글 기능을 하는 함수
function toggleSideMenu(event) {
	event.preventDefault(); // 기본 이벤트 동작을 막음
	const sideMenu = document.querySelector('.side-menu'); // side-menu 클래스를 가진 요소를 찾아서 저장
	const currentLeft = sideMenu.style.left; // 현재 사이드 메뉴의 left 위치값을 저장
	sideMenu.style.left = currentLeft === '0px' ? '-250px' : '0px'; // 현재 위치가 0px이면 -250px로, 아니면 0px로 설정하여 메뉴를 숨기거나 보여줌
}
if (!navigator.userAgent.includes("YourAppName")) {
    window.location.href = "http://127.0.0.1:5500/main-map/map.html#";
}

