#pragma once
#include "main.h"

// ����� ���� ��ȯ�ϴ� �Լ��� ���� �ʿ����� ������ ����� ¥�ÿ�
// ��, ���� Ranking ���� �߰� ����� ���� ������ �ְ� �����Ͻÿ�
// �⺻ �Լ� �ܿ� �ְ� ���� �Լ��� ����� �־ ¥�� ����

class GameManager {
	Player* user;
	Board* gameBoard;
	// �ð� ���� ��ü�� �˾Ƽ� ó��~
public:
	GameManager();
	
	void startGame();
	bool onGame();
	Player& finishGame();
};