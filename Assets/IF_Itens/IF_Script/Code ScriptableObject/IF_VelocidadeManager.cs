using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class VelocidadeData
{
    public string nome;
    public float velocidade;
}

[CreateAssetMenu(fileName = "VelocidadeManager", menuName = "Configs/VelocidadeManager")]
public class IF_VelocidadeManager : ScriptableObject
{
    public List<VelocidadeData> velocidades = new List<VelocidadeData>();

    public float GetVelocidade(string nome)
    {
        var v = velocidades.Find(x => x.nome == nome);
        return v != null ? v.velocidade : 0f;
    }

    public List<string> GetNomes()
    {
        List<string> nomes = new List<string>();
        foreach (var v in velocidades) nomes.Add(v.nome);
        return nomes;
    }
}
