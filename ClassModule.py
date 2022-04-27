# %%
import keras.models
import numpy as np
import matplotlib.pyplot as plt
import cv2
import os

CLASSES = ["Battery", "biological", "brown-glass", "cardboard", "clothes", "green-glass", "metal", "paper", "plastic", "shoes", "trash", "white-glass"]

def __GetImage(ImagePath):
    try:
        Image1 = cv2.imread(os.path.join(ImagePath), cv2.IMREAD_GRAYSCALE)
        Image2 = cv2.resize(Image1, (50, 50))
        return np.array(Image2).reshape(-1, 50, 50, 1)
    except:
        print("err")

def FindClass(ImgPath):
    try:
        Img = __GetImage(ImgPath)
        model = keras.models.load_model("GarbageClassifier.model")
        PredictionArr = model.predict([Img])
        Place = np.where(PredictionArr == 1)[1][0]
        return CLASSES[Place]
    except:
        print("err")


# %%



