<template>
    <div class="card filter-card mb-4">
        <div class="card-header">
            <h5 class="mb-0">
                <i class="bi bi-funnel me-2"></i>
                Filtros de Búsqueda
            </h5>
        </div>
        <div class="card-body">
            <div class="row g-3">
                <div class="col-md-2">
                    <label class="form-label">ID Job</label>
                    <select v-model="filters.idJob" class="form-select">
                        <option value="">Todos</option>
                        <option v-for="job in distinctJobs" :key="job" :value="job">{{ job }}</option>
                    </select>
                </div>
                <div class="col-md-2">
                    <label class="form-label">Día</label>
                    <input v-model="filters.dia" type="number" class="form-control" placeholder="Día">
                </div>
                <div class="col-md-2">
                    <label class="form-label">Fecha Inicio</label>
                    <input v-model="filters.fechaInicio" type="date" class="form-control">
                </div>
                <div class="col-md-2">
                    <label class="form-label">Fecha Término</label>
                    <input v-model="filters.fechaTermino" type="date" class="form-control">
                </div>
                <div class="col-md-2">
                    <label class="form-label">Resultado</label>
                    <select v-model="filters.resultado" class="form-select">
                        <option value="">Todos</option>
                        <option v-for="resultado in distinctResultados" :key="resultado" :value="resultado">
                            {{ resultado.length > 20 ? resultado.substring(0, 20) + '...' : resultado }}
                        </option>
                    </select>
                </div>
                <div class="col-md-2">
                    <label class="form-label">Nombre Archivo</label>
                    <input v-model="filters.nombreArchivo" type="text" class="form-control" placeholder="Archivo">
                </div>
            </div>
            <div class="row mt-3">
                <div class="col-md-6">
                    <label class="form-label">Tamaño de Página</label>
                    <select v-model="filters.pageSize" class="form-select">
                        <option value="25">25</option>
                        <option value="50">50</option>
                        <option value="100">100</option>
                        <option value="200">200</option>
                    </select>
                </div>
                <div class="col-md-6 d-flex align-items-end">
                    <button @click="applyFilters" class="btn btn-gradient me-2">
                        <i class="bi bi-search me-1"></i>
                        Buscar
                    </button>
                    <button @click="clearFilters" class="btn btn-outline-secondary">
                        <i class="bi bi-x-circle me-1"></i>
                        Limpiar
                    </button>
                </div>
            </div>
        </div>
    </div>
</template>

<script>
export default {
    props: {
        filters: {
            type: Object,
            required: true
        },
        distinctJobs: {
            type: Array,
            required: true
        },
        distinctResultados: {
            type: Array,
            required: true
        }
    },
    methods: {
        applyFilters() {
            this.$emit('apply-filters');
        },
        clearFilters() {
            this.$emit('clear-filters');
        }
    }
}
</script>

<style scoped>
.filter-card {
    background: rgba(102, 126, 234, 0.05);
    border: 1px solid rgba(102, 126, 234, 0.2);
}
</style>