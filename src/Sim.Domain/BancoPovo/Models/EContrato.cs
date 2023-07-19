using Sim.Domain.Entity;

namespace Sim.Domain.BancoPovo.Models;

public class EContrato
{

    public enum EnPagamento { Nulo = 0, Regular = 1, Inadimplente = 2, Liquidado = 3 }
    public enum EnSituacao { Analise = 0, Aprovado = 1, Reprovado = 2, Cancelado = 3 }
    public EContrato()
    {

    }

    public Guid Id { get; set; }
    public int Numero { get; set; }
    public DateTime? Data { get; set; }
    public decimal Valor { get; set; }
    public EnSituacao Situacao { get; set; }
    public DateTime? DataSituacao { get; set; }
    public Pessoa? Cliente { get; set; }
    public Empresas? Empresa { get; set; }
    public EnPagamento Pagamento { get; set; }
    public string? Descricao { get; set; }
    public string? AppUser { get; set; }
    public DateTime? UltimaAlteracao { get; set; }
    public virtual ICollection<ERenegociacoes>? Renegociacaoes { get; set; }

    #region Methods

    public bool ContratosLiquidados(EContrato obj)
    {
        if (obj.Situacao == EnSituacao.Aprovado && obj.Pagamento == EnPagamento.Liquidado)
            return true;
        else
            return false;
    }
    public bool ContratosCancelados(EContrato obj)
    {
        if (obj.Situacao == EnSituacao.Cancelado)
            return true;
        else
            return false;
    }
    public bool ContratosReprovados(EContrato obj)
    {
        if (obj.Situacao == EnSituacao.Reprovado)
            return true;
        else
            return false;
    }

    public bool ContratosEmAnalise(EContrato obj)
    {
        if (obj.Situacao == EnSituacao.Analise && obj.Pagamento == EnPagamento.Nulo)
            return true;
        else
            return false;
    }

    public bool ContratosAprovadosRegulares(EContrato obj)
    {
        if (obj.Situacao == EnSituacao.Aprovado && obj.Pagamento == EnPagamento.Regular && obj.Renegociacaoes!.Count() == 0)
            return true;
        else
            return false;
    }

    public bool ContratosAprovadosInadimplente(EContrato obj)
    {
        if (obj.Situacao == EnSituacao.Aprovado && obj.Pagamento == EnPagamento.Inadimplente)
            return true;
        else
            return false;
    }

    public bool ContratosRenegociados(EContrato obj)
    {
        if (obj.Renegociacaoes != null && obj.Renegociacaoes.Count > 0)
            return true;
        else
            return false;
    }

    public float TaxaInadimplencia(IEnumerable<EContrato> lista)
    {
        float _ret = 0.0F;
        float _regulares = lista.Where(s => s.ContratosAprovadosRegulares(s)).Count();
        float _inadimplentes = lista.Where(s => s.ContratosAprovadosInadimplente(s)).Count();
        _ret = _inadimplentes / _regulares;
        return _ret;
    }

    public decimal ValorContratosRegulares(IEnumerable<EContrato> lista)
    {
        var _valor = 0.0M;
        foreach (EContrato v in lista.Where(s => s.ContratosAprovadosRegulares(s)))
        {
            _valor += v.Valor;
        }
        return _valor;
    }

    public decimal ValorContratosInadimplentes(IEnumerable<EContrato> lista)
    {
        var _valor = 0.0M;
        foreach (EContrato v in lista.Where(s => s.ContratosAprovadosInadimplente(s)))
        {
            _valor += v.Valor;
        }
        return _valor;
    }

    public decimal ValorContratosAnalise(IEnumerable<EContrato> lista)
    {
        var _valor = 0.0M;
        foreach (EContrato v in lista.Where(s => s.ContratosEmAnalise(s)))
        {
            _valor += v.Valor;
        }
        return _valor;
    }

    public decimal ValorContratosRenegociados(IEnumerable<EContrato> lista)
    {
        var _valor = 0.0M;
        foreach (EContrato v in lista.Where(s => s.ContratosRenegociados(s)))
        {
            _valor += v.Valor;
        }
        return _valor;
    }

    public decimal ValorMedio(IEnumerable<EContrato> lista)
    {
        var _valor = 0.0M;
        var _cont = 0;

        foreach (EContrato v in lista.Where(s => s.ContratosRenegociados(s) || s.ContratosAprovadosRegulares(s)))
        {
            _valor += v.Valor;
            _cont++;
        }

        return _cont == 0 ? 0 : _valor / _cont;
    }

    #endregion


}