#include "main.h"

using namespace std;

Player::Player(string tempName = "�ٺ�") {
	// name�� �Ű������� ������ �־��ָ� ��
	x=0, y=0;// x�� y�� 0���� �ʱ�ȭ
}

int Player::getX() {
	return x; // x�� ��ȯ
}

int Player::getY() {
	return y;	// y�� ��ȯ
}

int Player::setX(int newx) {
	x=newx; // x�� ����
}

int Player::setY(int newy) {
	y=newy; // y�� ����
}

int Player::getName() {
	return name;// �̸� ��ȯ
}

int Player::setScore(int newscore) {
	score=newscore;// ���� ����
}

int Player::getScore() {
	return score;// ���� ��ȯ
}