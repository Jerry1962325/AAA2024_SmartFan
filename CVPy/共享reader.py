import mmap
str = '123'
byte = str.encode()
SHMEMSIZE = 10
file_name = 'test1'
print(SHMEMSIZE)
# python读取共享内存
shmem = mmap.mmap(0, SHMEMSIZE, file_name, mmap.ACCESS_READ)
print(shmem.read(SHMEMSIZE).decode('ASCII'))
shmem.close()