using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeManager : MonoBehaviour
{

    private struct PosData
    {

        public Vector3 position;
        public Quaternion rotation;
    }

    [SerializeField] private GameObject[] cubies;    
    [SerializeField] private float moveSpeed = 1;

    private int[] cubieData = {0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25};
    private List<string> moveBuffer = new List<string>();
    private PosData[] cubieInit = new PosData[26];
    private bool turning = false;
    void Start()
    {
        int i = 0;
        foreach (GameObject obj in cubies) {
            cubieInit[i].position = obj.transform.position;
            cubieInit[i].rotation = obj.transform.rotation;
            i++;
        }
    }

    void Update()
    {
        if (!turning && moveBuffer.Count != 0) ExecuteNextTurn();
        if (Input.GetKeyDown(KeyCode.R)) AddToBuffer("R");
        else if (Input.GetKeyDown(KeyCode.F)) AddToBuffer("RPrime");
        else if (Input.GetKeyDown(KeyCode.Q)) AddToBuffer("L");
        else if (Input.GetKeyDown(KeyCode.A)) AddToBuffer("LPrime");
        else if (Input.GetKeyDown(KeyCode.W)) AddToBuffer("U");
        else if (Input.GetKeyDown(KeyCode.E)) AddToBuffer("UPrime");
        else if (Input.GetKeyDown(KeyCode.D)) AddToBuffer("D");
        else if (Input.GetKeyDown(KeyCode.S)) AddToBuffer("DPrime");
        else if (Input.GetKeyDown(KeyCode.V)) AddToBuffer("F");
        else if (Input.GetKeyDown(KeyCode.C)) AddToBuffer("FPrime");
        else if (Input.GetKeyDown(KeyCode.Z)) AddToBuffer("B");
        else if (Input.GetKeyDown(KeyCode.X)) AddToBuffer("BPrime");
        else if (Input.GetKeyDown(KeyCode.Semicolon)) {
            AddToBuffer("R");
            AddToBuffer("U");
            AddToBuffer("RPrime");
            AddToBuffer("UPrime");
        }
        else if (Input.GetKeyDown(KeyCode.L)) {
            AddToBuffer("L");
            AddToBuffer("UPrime");
            AddToBuffer("LPrime");
            AddToBuffer("U");
        }
        else if (Input.GetKeyDown(KeyCode.P)) {
            AddToBuffer("RPrime");
            AddToBuffer("UPrime");
            AddToBuffer("R");
            AddToBuffer("U");
        }
        else if (Input.GetKeyDown(KeyCode.O)) {
            AddToBuffer("LPrime");
            AddToBuffer("U");
            AddToBuffer("L");
            AddToBuffer("UPrime");
        }
        else if (Input.GetKeyDown(KeyCode.Tab)) ResetCube();
    }

    private void ResetCube() {
        int i = 0;
        foreach (GameObject obj in cubies) {
            obj.transform.position = cubieInit[i].position;
            obj.transform.rotation = cubieInit[i].rotation;
            i++;
        }
        cubieData = new int[] {0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25};
    }

    private void ExecuteNextTurn() {
        switch (moveBuffer[0]) {
            case "R":
                StartCoroutine(R());
                break;
            case "RPrime":
                StartCoroutine(RPrime());
                break;
            case "L":
                StartCoroutine(L());
                break;
            case "LPrime":
                StartCoroutine(LPrime());
                break;
            case "U":
                StartCoroutine(U());
                break;
            case "UPrime":
                StartCoroutine(UPrime());
                break;
            case "D":
                StartCoroutine(D());
                break;
            case "DPrime":
                StartCoroutine(DPrime());
                break;
            case "F":
                StartCoroutine(F());
                break;
            case "FPrime":
                StartCoroutine(FPrime());
                break;
            case "B":
                StartCoroutine(B());
                break;
            case "BPrime":
                StartCoroutine(BPrime());
                break;
        }
        moveBuffer.Remove(moveBuffer[0]);
        moveBuffer.TrimExcess();
    }

    private void AddToBuffer(string move) {
        moveBuffer.Add(move);
    }

    private IEnumerator R() {
        turning = true;
        GameObject[] affected = {cubies[cubieData[2]], cubies[cubieData[5]], cubies[cubieData[8]], cubies[cubieData[11]], cubies[cubieData[13]], cubies[cubieData[16]], cubies[cubieData[19]], cubies[cubieData[22]], cubies[cubieData[25]]};
        float timer = 0;
        Vector3[] targetPos = new Vector3[9];
        Vector3[] initPos = new Vector3[9];
        Quaternion[] targetRot = new Quaternion[9];
        Quaternion[] initRot = new Quaternion[9];
        int i = 0;
        foreach(GameObject obj in affected) {
            initPos[i] = obj.transform.position;
            initRot[i] = obj.transform.rotation;
            targetPos[i] = (Quaternion.Euler(0, 0, -90) * (obj.transform.position - cubies[cubieData[13]].transform.position));
            targetRot[i] = obj.transform.rotation * Quaternion.Euler(0, 0, -90);
            i++;
        }
        while (timer < (1 / moveSpeed)) {
            i = 0;
            foreach(GameObject obj in affected) {
                obj.transform.position = Vector3.Lerp(initPos[i], targetPos[i], timer * moveSpeed);
                obj.transform.rotation = Quaternion.Lerp(initRot[i], targetRot[i], timer * moveSpeed);
                i++;
            }
            timer += Time.deltaTime;
            yield return null;
        }
        i = 0;
        foreach(GameObject obj in affected) {
            obj.transform.position = targetPos[i];
            obj.transform.rotation = targetRot[i];
            i++;
        }
        cubieData = new int[] {cubieData[0], cubieData[1], cubieData[8], cubieData[3], cubieData[4], cubieData[16], cubieData[6], cubieData[7], cubieData[25], cubieData[9], cubieData[10], cubieData[5], cubieData[12], cubieData[13], cubieData[14], cubieData[15], cubieData[22], cubieData[17], cubieData[18], cubieData[2], cubieData[20], cubieData[21], cubieData[11], cubieData[23], cubieData[24], cubieData[19]};
        turning = false;
    }

    private IEnumerator RPrime() {
        turning = true;
        GameObject[] affected = {cubies[cubieData[2]], cubies[cubieData[5]], cubies[cubieData[8]], cubies[cubieData[11]], cubies[cubieData[13]], cubies[cubieData[16]], cubies[cubieData[19]], cubies[cubieData[22]], cubies[cubieData[25]]};
        float rot = 0;
        while (rot < 90) {
            foreach(GameObject obj in affected) {
                obj.transform.RotateAround(cubies[cubieData[13]].transform.position, Vector3.forward, 90 * Time.deltaTime * moveSpeed);
            }
            rot += 90 * Time.deltaTime * moveSpeed;
            yield return null;
        }
        cubieData = new int[] {cubieData[0], cubieData[1], cubieData[19], cubieData[3], cubieData[4], cubieData[11], cubieData[6], cubieData[7], cubieData[2], cubieData[9], cubieData[10], cubieData[22], cubieData[12], cubieData[13], cubieData[14], cubieData[15], cubieData[5], cubieData[17], cubieData[18], cubieData[25], cubieData[20], cubieData[21], cubieData[16], cubieData[23], cubieData[24], cubieData[8]};
        turning = false;
    }

    private IEnumerator L() {
        turning = true;
        GameObject[] affected = {cubies[cubieData[0]], cubies[cubieData[3]], cubies[cubieData[6]], cubies[cubieData[9]], cubies[cubieData[12]], cubies[cubieData[14]], cubies[cubieData[17]], cubies[cubieData[20]], cubies[cubieData[23]]};
        float rot = 0;
        while (rot < 90) {
            foreach(GameObject obj in affected) {
                obj.transform.RotateAround(cubies[cubieData[12]].transform.position, Vector3.forward, -90 * Time.deltaTime * moveSpeed);
            }
            rot += 90 * Time.deltaTime * moveSpeed;
            yield return null;
        }
        cubieData = new int[] {cubieData[6], cubieData[1], cubieData[2], cubieData[14], cubieData[4], cubieData[5], cubieData[23], cubieData[7], cubieData[8], cubieData[3], cubieData[10], cubieData[11], cubieData[12], cubieData[13], cubieData[20], cubieData[15], cubieData[16], cubieData[0], cubieData[18], cubieData[19], cubieData[9], cubieData[21], cubieData[22], cubieData[17], cubieData[24], cubieData[25]};
        turning = false;
    }

    private IEnumerator LPrime() {
        turning = true;
        GameObject[] affected = {cubies[cubieData[0]], cubies[cubieData[3]], cubies[cubieData[6]], cubies[cubieData[9]], cubies[cubieData[12]], cubies[cubieData[14]], cubies[cubieData[17]], cubies[cubieData[20]], cubies[cubieData[23]]};
        float rot = 0;
        while (rot < 90) {
            foreach(GameObject obj in affected) {
                obj.transform.RotateAround(cubies[cubieData[12]].transform.position, Vector3.forward, 90 * Time.deltaTime * moveSpeed);
            }
            rot += 90 * Time.deltaTime * moveSpeed;
            yield return null;
        }
        cubieData = new int[] {cubieData[17], cubieData[1], cubieData[2], cubieData[9], cubieData[4], cubieData[5], cubieData[0], cubieData[7], cubieData[8], cubieData[20], cubieData[10], cubieData[11], cubieData[12], cubieData[13], cubieData[3], cubieData[15], cubieData[16], cubieData[23], cubieData[18], cubieData[19], cubieData[14], cubieData[21], cubieData[22], cubieData[6], cubieData[24], cubieData[25]};
        turning = false;
    }
    
    private IEnumerator U() {
        turning = true;
        GameObject[] affected = {cubies[cubieData[17]], cubies[cubieData[18]], cubies[cubieData[19]], cubies[cubieData[20]], cubies[cubieData[21]], cubies[cubieData[22]], cubies[cubieData[23]], cubies[cubieData[24]], cubies[cubieData[25]]};
        float rot = 0;
        while (rot < 90) {
            foreach(GameObject obj in affected) {
                obj.transform.RotateAround(cubies[cubieData[21]].transform.position, Vector3.up, 90 * Time.deltaTime * moveSpeed);
            }
            rot += 90 * Time.deltaTime * moveSpeed;
            yield return null;
        }
        cubieData = new int[] {cubieData[0], cubieData[1], cubieData[2], cubieData[3], cubieData[4], cubieData[5], cubieData[6], cubieData[7], cubieData[8], cubieData[9], cubieData[10], cubieData[11], cubieData[12], cubieData[13], cubieData[14], cubieData[15], cubieData[16], cubieData[19], cubieData[22], cubieData[25], cubieData[18], cubieData[21], cubieData[24], cubieData[17], cubieData[20], cubieData[23]};
        turning = false;
    }

    private IEnumerator UPrime() {
        turning = true;
        GameObject[] affected = {cubies[cubieData[17]], cubies[cubieData[18]], cubies[cubieData[19]], cubies[cubieData[20]], cubies[cubieData[21]], cubies[cubieData[22]], cubies[cubieData[23]], cubies[cubieData[24]], cubies[cubieData[25]]};
        float rot = 0;
        while (rot < 90) {
            foreach(GameObject obj in affected) {
                obj.transform.RotateAround(cubies[cubieData[21]].transform.position, Vector3.up, -90 * Time.deltaTime * moveSpeed);
            }
            rot += 90 * Time.deltaTime * moveSpeed;
            yield return null;
        }
        cubieData = new int[] {cubieData[0], cubieData[1], cubieData[2], cubieData[3], cubieData[4], cubieData[5], cubieData[6], cubieData[7], cubieData[8], cubieData[9], cubieData[10], cubieData[11], cubieData[12], cubieData[13], cubieData[14], cubieData[15], cubieData[16], cubieData[23], cubieData[20], cubieData[17], cubieData[24], cubieData[21], cubieData[18], cubieData[25], cubieData[22], cubieData[19]};
        turning = false;
    }

    private IEnumerator D() {
        turning = true;
        GameObject[] affected = {cubies[cubieData[0]], cubies[cubieData[1]], cubies[cubieData[2]], cubies[cubieData[3]], cubies[cubieData[4]], cubies[cubieData[5]], cubies[cubieData[6]], cubies[cubieData[7]], cubies[cubieData[8]]};
        float rot = 0;
        while (rot < 90) {
            foreach(GameObject obj in affected) {
                obj.transform.RotateAround(cubies[cubieData[4]].transform.position, Vector3.up, -90 * Time.deltaTime * moveSpeed);
            }
            rot += 90 * Time.deltaTime * moveSpeed;
            yield return null;
        }
        cubieData = new int[] {cubieData[6], cubieData[3], cubieData[0], cubieData[7], cubieData[4], cubieData[1], cubieData[8], cubieData[5], cubieData[2], cubieData[9], cubieData[10], cubieData[11], cubieData[12], cubieData[13], cubieData[14], cubieData[15], cubieData[16], cubieData[17], cubieData[18], cubieData[19], cubieData[20], cubieData[21], cubieData[22], cubieData[23], cubieData[24], cubieData[25]};
        turning = false;
    }

    private IEnumerator DPrime() {
        turning = true;
        GameObject[] affected = {cubies[cubieData[0]], cubies[cubieData[1]], cubies[cubieData[2]], cubies[cubieData[3]], cubies[cubieData[4]], cubies[cubieData[5]], cubies[cubieData[6]], cubies[cubieData[7]], cubies[cubieData[8]]};
        float rot = 0;
        while (rot < 90) {
            foreach(GameObject obj in affected) {
                obj.transform.RotateAround(cubies[cubieData[4]].transform.position, Vector3.up, 90 * Time.deltaTime * moveSpeed);
            }
            rot += 90 * Time.deltaTime * moveSpeed;
            yield return null;
        }
        cubieData = new int[] {cubieData[2], cubieData[5], cubieData[8], cubieData[1], cubieData[4], cubieData[7], cubieData[0], cubieData[3], cubieData[6], cubieData[9], cubieData[10], cubieData[11], cubieData[12], cubieData[13], cubieData[14], cubieData[15], cubieData[16], cubieData[17], cubieData[18], cubieData[19], cubieData[20], cubieData[21], cubieData[22], cubieData[23], cubieData[24], cubieData[25]};
        turning = false;
    }

    private IEnumerator F() {
        turning = true;
        GameObject[] affected = {cubies[cubieData[0]], cubies[cubieData[1]], cubies[cubieData[2]], cubies[cubieData[9]], cubies[cubieData[10]], cubies[cubieData[11]], cubies[cubieData[17]], cubies[cubieData[18]], cubies[cubieData[19]]};
        float rot = 0;
        while (rot < 90) {
            foreach(GameObject obj in affected) {
                obj.transform.RotateAround(cubies[cubieData[10]].transform.position, Vector3.right, -90 * Time.deltaTime * moveSpeed);
            }
            rot += 90 * Time.deltaTime * moveSpeed;
            yield return null;
        }
        cubieData = new int[] {cubieData[2], cubieData[11], cubieData[19], cubieData[3], cubieData[4], cubieData[5], cubieData[6], cubieData[7], cubieData[8], cubieData[1], cubieData[10], cubieData[18], cubieData[12], cubieData[13], cubieData[14], cubieData[15], cubieData[16], cubieData[0], cubieData[9], cubieData[17], cubieData[20], cubieData[21], cubieData[22], cubieData[23], cubieData[24], cubieData[25]};
        turning = false;
    }

    private IEnumerator FPrime() {
        turning = true;
        GameObject[] affected = {cubies[cubieData[0]], cubies[cubieData[1]], cubies[cubieData[2]], cubies[cubieData[9]], cubies[cubieData[10]], cubies[cubieData[11]], cubies[cubieData[17]], cubies[cubieData[18]], cubies[cubieData[19]]};
        float rot = 0;
        while (rot < 90) {
            foreach(GameObject obj in affected) {
                obj.transform.RotateAround(cubies[cubieData[10]].transform.position, Vector3.right, 90 * Time.deltaTime * moveSpeed);
            }
            rot += 90 * Time.deltaTime * moveSpeed;
            yield return null;
        }
        cubieData = new int[] {cubieData[17], cubieData[9], cubieData[0], cubieData[3], cubieData[4], cubieData[5], cubieData[6], cubieData[7], cubieData[8], cubieData[18], cubieData[10], cubieData[1], cubieData[12], cubieData[13], cubieData[14], cubieData[15], cubieData[16], cubieData[19], cubieData[11], cubieData[2], cubieData[20], cubieData[21], cubieData[22], cubieData[23], cubieData[24], cubieData[25]};
        turning = false;
    }

    private IEnumerator B() {
        turning = true;
        GameObject[] affected = {cubies[cubieData[6]], cubies[cubieData[7]], cubies[cubieData[8]], cubies[cubieData[14]], cubies[cubieData[15]], cubies[cubieData[16]], cubies[cubieData[23]], cubies[cubieData[24]], cubies[cubieData[25]]};
        float rot = 0;
        while (rot < 90) {
            foreach(GameObject obj in affected) {
                obj.transform.RotateAround(cubies[cubieData[15]].transform.position, Vector3.right, 90 * Time.deltaTime * moveSpeed);
            }
            rot += 90 * Time.deltaTime * moveSpeed;
            yield return null;
        }
        cubieData = new int[] {cubieData[0], cubieData[1], cubieData[2], cubieData[3], cubieData[4], cubieData[5], cubieData[23], cubieData[14], cubieData[6], cubieData[9], cubieData[10], cubieData[11], cubieData[12], cubieData[13], cubieData[24], cubieData[15], cubieData[7], cubieData[17], cubieData[18], cubieData[19], cubieData[20], cubieData[21], cubieData[22], cubieData[25], cubieData[16], cubieData[8]};
        turning = false;
    }

    private IEnumerator BPrime() {
        turning = true;
        GameObject[] affected = {cubies[cubieData[6]], cubies[cubieData[7]], cubies[cubieData[8]], cubies[cubieData[14]], cubies[cubieData[15]], cubies[cubieData[16]], cubies[cubieData[23]], cubies[cubieData[24]], cubies[cubieData[25]]};
        float rot = 0;
        while (rot < 90) {
            foreach(GameObject obj in affected) {
                obj.transform.RotateAround(cubies[cubieData[15]].transform.position, Vector3.right, -90 * Time.deltaTime * moveSpeed);
            }
            rot += 90 * Time.deltaTime * moveSpeed;
            yield return null;
        }
        cubieData = new int[] {cubieData[0], cubieData[1], cubieData[2], cubieData[3], cubieData[4], cubieData[5], cubieData[8], cubieData[16], cubieData[25], cubieData[9], cubieData[10], cubieData[11], cubieData[12], cubieData[13], cubieData[7], cubieData[15], cubieData[24], cubieData[17], cubieData[18], cubieData[19], cubieData[20], cubieData[21], cubieData[22], cubieData[6], cubieData[14], cubieData[23]};
        turning = false;
    }
}
