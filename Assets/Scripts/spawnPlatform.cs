using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnPlatform : MonoBehaviour
{
    public List<GameObject> platforms = new List<GameObject>(); //lista dos prefabs
    public List<Transform> currentPlatforms = new List<Transform>(); //lista dos objetos
    public int offset; //controla a distancia das plataformas

    private Transform player;
    private Transform currentFinalPoint;
    private int platformIndex;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;

        //enquanto a var i for menor que platforms count o i incrementa com o ++
        //isso faz um loop e quando o i for maior que o count ele volta pra zero
        for (int i = 0; i < platforms.Count; i++)
        {

            //o vector3 pega o objeto i e multiplica 86 na posicao Z 
            //(isso spawna uma plataforma colada na outra)
            Transform p = Instantiate(platforms[i], new Vector3(0, 0, i * 86), transform.rotation).transform;
            currentPlatforms.Add(p);
            offset += 86;
        }
        //aqui eu ja comeco recebendo a plataforma a ou seja [0]
        currentFinalPoint = currentPlatforms[platformIndex].GetComponent<Platform>().point;
    }

    void Update()
    {
        float distance = player.position.z - currentFinalPoint.position.z;
        if (distance >= 5)
        {

            Recycle(currentPlatforms[platformIndex].gameObject);
            platformIndex++;

            if (platformIndex > currentPlatforms.Count - 1)
            {
                platformIndex = 0;
            }

            currentFinalPoint = currentPlatforms[platformIndex].GetComponent<Platform>().point;
        }
    }

    public void Recycle(GameObject platf)
    {
        platf.transform.position = new Vector3(0, 0, offset); //aqui o offset já pegou a ultima posicao da plataforma
        offset += 86;//aqui eu atualizo o offset
    }
}
