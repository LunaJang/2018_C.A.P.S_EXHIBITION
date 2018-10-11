#include "main.h"

using namespace std;

Board::Board() {
	width = 10;
	height = 10;
	map.resize(width, vector<int>(height));
	// width(맵의 가로 길이), height(맵의 세로 길이) 정해주기, 위의 멤버 변수 사용
	// map에 자료 넣기
}

bool Board::canMove(int x, int y, int moveX, int moveY) {
	// x, y는 player의 현재 좌표
	// moveX, moveY는 player의 이동 좌표 ( 1,0 또는 -1,0 등등)
	// map의 상태를 보고 이동이 가능하면 true 불가능하면 false 반환
}

bool Board::isArrived(int x, int y) {
	if (x == 10 && y == 10)
		return true;
	else
		return false;
}

vector<vector<int>>& Board::getMap(int x, int y) {
	// x, y는 player의 현재 좌표
	// 현재 좌표를 중심으로 위, 아래, 왼쪽, 오른쪽 10칸씩만 벡터를 잘라서 반환할것
	// map을 자르는게 아니라 반환 시킬 벡터를 새로 만들어서 반환할것
}

