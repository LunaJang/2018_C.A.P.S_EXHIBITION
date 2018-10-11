#include "main.h"

using namespace std;

Player::Player(string tempName = "바보") {
	// name에 매개변수로 받은거 넣어주면 됨
	x=0, y=0;// x랑 y는 0으로 초기화
}

int Player::getX() {
	return x; // x값 반환
}

int Player::getY() {
	return y;	// y값 반환
}

int Player::setX(int newx) {
	x=newx; // x값 설정
}

int Player::setY(int newy) {
	y=newy; // y값 설정
}

int Player::getName() {
	return name;// 이름 반환
}

int Player::setScore(int newscore) {
	score=newscore;// 점수 설정
}

int Player::getScore() {
	return score;// 점수 반환
}