import mmap
import time
str = '123456'
byte = str.encode(encoding='UTF-8')
SHMEMSIZE = len(str)

rewrite = '\0'*11
file_name = 'global_share_memory'
print(file_name)

# python写入共享内存
shmem = mmap.mmap(0, 10, file_name, mmap.ACCESS_WRITE)
shmem.write(byte)

for i in range(1,100):
  
  tmp = time.strftime('%H:%M:%S', time.localtime())
  print(tmp)
  shmem = mmap.mmap(0, 11, file_name, mmap.ACCESS_WRITE)
  shmem.write(tmp.encode(encoding='UTF-8'))
  time.sleep(3)
  shmem = mmap.mmap(0, 11, file_name, mmap.ACCESS_WRITE)
  shmem.write(rewrite.encode(encoding='UTF-8'))
"""
  tmp = input("请输入新内容：")+'\0'
  shmem = mmap.mmap(0, len(tmp), file_name, mmap.ACCESS_WRITE)
  shmem.write(tmp.encode(encoding='UTF-8'))
"""