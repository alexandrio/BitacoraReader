<template>
    <div class="card">
        <div class="card-header">
            <h5 class="mb-0">
                <i class="bi bi-table me-2"></i>
                Resultados de Bitácoras
            </h5>
        </div>
        <div class="card-body p-0">
            <div class="table-responsive">
                <table class="table table-striped table-hover mb-0">
                    <thead class="table-dark">
                        <tr>
                            <th>ID</th>
                            <th>Job</th>
                            <th>Día</th>
                            <th>Hora</th>
                            <th>Archivo</th>
                            <th>Fecha Inicio</th>
                            <th>Fecha Término</th>
                            <th>Duración</th>
                            <th>Estado</th>
                            <th>Resultado</th>
                            <th>Acciones</th>
                        </tr>
                    </thead>
                    <tbody>
                        <tr v-for="bitacora in pagedResult.items" :key="bitacora.idBitacora">
                            <td>{{ bitacora.idBitacora }}</td>
                            <td>{{ bitacora.idJob }}</td>
                            <td>{{ bitacora.dia }}</td>
                            <td>{{ formatTime(bitacora.hora) }}</td>
                            <td>
                                <span :title="bitacora.nombreArchivo">
                                    {{ bitacora.nombreArchivo.length > 20 ? bitacora.nombreArchivo.substring(0, 20) + '...' : bitacora.nombreArchivo }}
                                </span>
                            </td>
                            <td>{{ formatDateTime(bitacora.fechaInicio) }}</td>
                            <td>{{ formatDateTime(bitacora.fechaTermino) }}</td>
                            <td>{{ bitacora.duracionFormateada || 'N/A' }}</td>
                            <td>
                                <span class="badge status-badge" :class="getStatusClass(bitacora)">
                                    {{ getStatusText(bitacora) }}
                                </span>
                            </td>
                            <td>
                                <span :title="bitacora.resultado" class="text-truncate d-inline-block" style="max-width: 200px;">
                                    {{ bitacora.resultado }}
                                </span>
                            </td>
                            <td>
                                <button @click="viewDetail(bitacora)" class="btn btn-sm btn-outline-primary">
                                    <i class="bi bi-eye"></i>
                                </button>
                            </td>
                        </tr>
                        <tr v-if="pagedResult.items.length === 0">
                            <td colspan="11" class="text-center py-4">
                                <i class="bi bi-inbox text-muted fs-1"></i>
                                <p class="text-muted mt-2">No se encontraron registros</p>
                            </td>
                        </tr>
                    </tbody>
                </table>
            </div>
        </div>
    </div>
</template>

<script>
export default {
    props: {
        pagedResult: {
            type: Object,
            required: true
        },
        formatDateTime: {
            type: Function,
            required: true
        },
        formatTime: {
            type: Function,
            required: true
        },
        getStatusClass: {
            type: Function,
            required: true
        },
        getStatusText: {
            type: Function,
            required: true
        },
        viewDetail: {
            type: Function,
            required: true
        }
    }
}
</script>

<style scoped>
.status-badge {
    font-size: 0.75rem;
}
</style>