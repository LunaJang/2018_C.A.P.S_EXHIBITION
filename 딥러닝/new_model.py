
# coding: utf-8

# ## Import library

# In[1]:


# PyTorch 라이브러리 임포트
import torch
from torch.autograd import Variable
import torch.nn as nn
import torch.nn.functional as F
import torch.optim as optim
from torch.utils.data import DataLoader, TensorDataset
import numpy as np
from os import walk
import matplotlib.pyplot as plt

# scikit-learn 라이브러리 임포트
from sklearn.model_selection import train_test_split
import pickle


# ## Net 정의

# In[2]:


# 신경망 구성
class Net(nn.Module):
    def __init__(self):
        super(Net, self).__init__()
        # 합성곱층
        self.conv1 = nn.Conv2d(1, 6, 5) # 입력 채널 수, 출력 채널 수, 필터 크기
        self.conv2 = nn.Conv2d(6, 16, 5)
        # 전결합층
        self.fc1 = nn.Linear(256, 64) # 256 = (((28-5+1)/2 )-5+1)/2 * (((28-5+1)/2 )-5+1)/2 * 16
        self.fc2 = nn.Linear(64, 10)
    
    def forward(self, x):
        # 풀링층
        x = F.max_pool2d(F.relu(self.conv1(x)), 2) # 풀링 영역 크기
        x = F.max_pool2d(F.relu(self.conv2(x)), 2)
        x = x.view(-1, 256)
        x = F.relu(self.fc1(x))
        x = self.fc2(x)
        return F.log_softmax(x,dim=1)


# ## unpickling

# In[3]:


with open('Model.pkl','rb') as f:
    model = pickle.load(f)


# ## C#에서 string 받고 C#에 result보내기

# In[4]:


s='1,1,1,0,0,0,0'
ex = np.fromstring(s, sep=",",dtype='float')
print(ex)
#ex1=ex.reshape(1,1,28,28)
#ex2=torch.from_numpy(a).float()
#result=model(ex2)
#print(result)

