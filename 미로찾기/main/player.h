#pragma once
#include "main.h"

// ��¥ �� �����ִ� �Լ��� ����� ��
// �Ű������� �˾Ƽ� �����ÿ�

class Player {
	int x, y;
	string name;
	int score;
public:
	Player(string tempName = "�ٺ�");

	int getX();

	int getY();

	int setX();


	int setY();

	int getName();

	int setScore();

	int getScore();
};