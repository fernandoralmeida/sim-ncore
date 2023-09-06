using System.Globalization;

namespace Sim.Application.Indicadores.VModel;

public record VmREventos(
    KeyValuePair<string, int>? EventosP,
    KeyValuePair<string, int>? EventosR,
    KeyValuePair<string, int>? EventosC,
    IEnumerable<KeyValuePair<string, int>>? Eventos,
    KeyValuePair<string, int>? Inscritos,
    KeyValuePair<string, int>? Presentes,
    IEnumerable<KeyValuePair<string, int>>? FaixaEtaria,
    IEnumerable<KeyValuePair<string, int>>? FaixaEtariaPresentes,
    IEnumerable<KeyValuePair<string, int>>? ParticipantesGenero,
    IEnumerable<KeyValuePair<string, int>>? ParticipantesGeneroPresente,
    IEnumerable<KeyValuePair<string, int>>? EventosSetores,
    IEnumerable<KeyValuePair<string, int>>? EventosMeses,
    IEnumerable<KeyValuePair<string, int>>? EventosMesesInscritos,
    IEnumerable<KeyValuePair<string, int>>? EventosMesesParticipantes
);
