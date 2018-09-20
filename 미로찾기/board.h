#pragma once
#include "main.h"

// Board안의 변수 값(mpa, x, y)들은 변경 되어서는 안됨!!
// 절대 set함수를 만들지 말것
// 장애물은 아직 만들지 말 것

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