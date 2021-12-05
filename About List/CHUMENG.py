import random, string
import os
import io
import textwrap
import configparser
import base64
print("正在开启\n开启成功!本软件作者YUXUAN")
a = input("请输入")
def random_str(slen=11):
    seed = "1234567890abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ!@#$%^&*()_+=-"
    sa = []
    for i in range(slen):
      sa.append(random.choice(seed))
    return ''.join(sa)

def encode(s):
    return ' '.join([bin(ord(c)).replace('0b', '') for c in s])
 
def decode(s):
    return ''.join([chr(i) for i in [int(b, 2) for b in s.split(' ')]])

if(a == "s"):
    print("开始")
    Token = random_str() #Token
    MiToken = encode(Token)
    bs=str(base64.b64encode(MiToken.encode('utf-8')),"utf-8")
    print("Token:"+Token+"加密:"+ bs)
    ########os.system('killall -8 frps')
    #with open("./token/token.txt","w") as f:
  #      f.write(MiToken) 
#*
   # cfp = configparser.ConfigParser()
#cfp.read("token.ini")
#
#cfp.add_section("common")  # 设置option的值
#cfp.set("common", "Token",Token) 
#with open("token.ini", "w+") as f:
   # cfp.write(f)
#*
   