#include "main.h"

using namespace std;

Board::Board() {
	width = 5;
	height = 5;
	map.resize(width, vector<int>(height));
	map={{0,0,1,1,1},{1,0,0,0,1},{0,0,0,1,1},{0,1,0,0,1},{1,1,1,0,0}}
	// width(���� ���� ����), height(���� ���� ����) �����ֱ�, ���� ��� ���� ���
	// map�� �ڷ� �ֱ�
}

bool Board::canMove(int x, int y, int moveX, int moveY) {
	// x, y�� player�� ���� ��ǥ
	// moveX, moveY�� player�� �̵� ��ǥ ( 1,0 �Ǵ� -1,0 ���)
	// map�� ���¸� ���� �̵��� �����ϸ� true �Ұ����ϸ� false ��ȯ
}

bool Board::isArrived(int x, int y) {
	if (x == width && y == height)
		return true;
	else
		return false;
}

vector<vector<int>>& Board::getMap(int x, int y) {
	// x, y�� player�� ���� ��ǥ
	// ���� ��ǥ�� �߽����� ��, �Ʒ�, ����, ������ 10ĭ���� ���͸� �߶� ��ȯ�Ұ�
	// map�� �ڸ��°� �ƴ϶� ��ȯ ��ų ���͸� ���� ���� ��ȯ�Ұ�
}

