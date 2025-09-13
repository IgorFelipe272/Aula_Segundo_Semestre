using UnityEngine;
using UnityEngine.AI;

public class Aula_EnemyChase : MonoBehaviour
{
    private NavMeshAgent agent;
    private Transform player;

    void Start()
    {
        // Pega o NavMeshAgent do inimigo
        agent = GetComponent<NavMeshAgent>();

        // Encontra o jogador pela tag "Player"
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        {
            player = playerObj.transform;
        }
        else
        {
            Debug.LogWarning("Player n�o encontrado na cena! Certifique-se de que o jogador tenha a tag 'Player'.");
        }
    }

    void Update()
    {
        if (player != null)
        {
            // Atualiza o destino do agente para a posi��o do jogador
            agent.SetDestination(player.position);
        }
    }
}
