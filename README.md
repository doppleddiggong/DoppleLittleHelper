# DoppleLittleHelper

- 유니티 툴바 확장을 이용한 유니티 필요 기능 지원

![image.png](https://github.com/user-attachments/assets/24e0c4af-6878-4464-ac09-e34f848fad79)

### 사용법

![image.png](https://github.com/user-attachments/assets/6787f0ee-7ee6-493c-a44d-467574e00c78)

플레이 시작시 **DoppleLittleHelper** 폴더에 **DoppleLittleHelper.SO**  파일 생성

ScriptableObject 데이터 제어를 통해 옵션을 조정해서 사용

![image.png](https://github.com/user-attachments/assets/345e3c91-e7ed-400b-b8e0-5a107c94f954)

- **[툴바 버튼 색상 제어] :** 상단 툴바의 버튼 색깔 제어
    - **ColorLeftArea** :  좌측 버튼 그룹의 색상 조정
    - **ColorRightArea** : 우측 버튼 그룹의 색상 조정
- **[타일 스케일 제어] :** 지정된 **TimeScaleList**를 순회하면서 **TimeScale** 제어
    - **TimeScaleList** : 조정 필요한 TimeScaleList
    - **TimeScaleStartIndex** : **TimeScaleList**의 배열 시작값
- **[스크린 샷 제어]**
    - **Folder_Dir** :  **Game**의 스크린샷이 저장될 폴더명, ***Asset/../{FolderDir}***으로 저장.
    - **Prefix** : 저장된 파일 접두어
    - **Use_Date_Time** : ***DateTime.Now***를 사용해서 **File_format** 형식으로 **png** 저장
    - **Capture_Size** :  스크린샷 저장시, 저장 이미지 배율 조정값

### 좌측 영역

![image.png](https://github.com/user-attachments/assets/58f8893b-4543-41dc-9274-e133f0c53e9b)

- 유니티의 Time.timeScale을 조정하여 유니티의 플레이 속도를 제어

### 우측 영역

![image.png](https://github.com/user-attachments/assets/6bdad2af-a9c4-4613-ac9e-73183edc8b5e)

- **Start First Scene**
    - 유니티 Build Profile의 Scene List의 첫번째 씬을 시작씬으로 PlayMode에 진입
- **Screen Capture**
    - Game씬을 캡쳐하여, /../ScreenShot 폴더에 이미지 파일을 저장
- **Open Capture Folder**
    - /../ScreenShot 폴더를 열어준다
- **Open UserData Folder**
    - Application.persistentDataPath 폴더를 열어준다
- **Open Editor.Log**
    - Editor.Log 파일을 열어준다
- **Open ScriptTemplates**
    - ScriptTemplates 폴더를 열어준다

### 설치

- Click **Window** > **Package Manager** to **open Package Manager UI.**
- Click **+** > **Add package from git URL**... and input the repository URL : [](https://github.com/doppleddiggong/DoppleLittleHelper.git)
    
    [https://github.com/doppleddiggong/DoppleLittleHelper.git](https://github.com/doppleddiggong/DoppleLittleHelper.git)

![image.png](https://github.com/user-attachments/assets/a19a7528-aa17-4964-a7bf-c8727faa1d08)

### 포함된 에셋

[https://github.com/marijnz/unity-toolbar-extender](https://github.com/marijnz/unity-toolbar-extender)

### **License**

DoppleLittleHelper is [MIT licensed](https://www.notion.so/dopple/LICENSE.md).