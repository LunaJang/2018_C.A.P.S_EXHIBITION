#pragma once
#include "main.h"

// Board���� ���� ��(mpa, x, y)���� ���� �Ǿ�� �ȵ�!!
// ���� set�Լ��� ������ ����
// ��ֹ��� ���� ������ �� ��

class Board {
	static vector<vector <int>> map;
	int width;
	int height;

public:
	Board();

	bool canMove(int x, int y, int moveX, int moveY);
	bool isArrived(int x, int y);
	vector<vector<int>>& getMap(int x, int y);
};