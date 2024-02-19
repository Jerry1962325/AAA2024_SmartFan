import cv2
import HandTrackingModule as htm
import time

#############################
wCam, hCam = 640, 480
#############################
cap = cv2.VideoCapture(0)  # 若使用笔记本自带摄像头则编号为0  若使用外接摄像头 则更改为1或其他编号
cap.set(3, wCam)
cap.set(4, hCam)
pTime = 0
detector = htm.handDetector()

while True:
    success, img = cap.read()
    img = detector.findHands(img)
    lmList = detector.findPosition(img, draw=False)
    pointList = [4, 8, 12, 16, 20]
    if len(lmList) != 0:
        countList = []
        # 大拇指
        if lmList[4][1] > lmList[3][1]:
            countList.append(1)
        else:
            countList.append(0)
        # 余下四个手指
        for i in range(1, 5):
            if lmList[pointList[i]][2] < lmList[pointList[i] - 2][2]:
                countList.append(1)
            else:
                countList.append(0)
        print(countList)

        count = countList.count(1)  # 对列表中含有的1计数
        ##HandImage = cv2.imread(f'FingerImg/{count}.jpg')
        ##HandImage = cv2.resize(HandImage, (150, 200))
        ##h, w, c = HandImage.shape
        ##img[0:h, 0:w] = HandImage
        cv2.putText(img, f'{int(count)}', (15, 400), cv2.FONT_HERSHEY_PLAIN, 15, (255, 0, 255), 10)


    cTime = time.time()
    fps = 1 / (cTime - pTime)
    pTime = cTime
    cv2.putText(img, f'fps: {int(fps)}', (0, 40), cv2.FONT_HERSHEY_PLAIN, 2, (255, 0, 0), 2)
    cv2.imshow("Image", img)
    if cv2.waitKey(1)==ord('q'):
        cv2.destroyAllWindows()
        break

    