<template>
  <div class="row mb-4">
    <div class="col-md-3" v-for="(stat, index) in stats" :key="index">
      <div class="card stats-card">
        <div class="card-body text-center">
          <h3 class="mb-0">{{ stat.value.toLocaleString() }}</h3>
          <p class="mb-0">{{ stat.label }}</p>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  props: {
    totalRecords: {
      type: Number,
      required: true
    },
    distinctJobs: {
      type: Number,
      required: true
    },
    distinctResultados: {
      type: Number,
      required: true
    },
    filteredRecords: {
      type: Number,
      required: true
    }
  },
  computed: {
    stats() {
      return [
        { value: this.totalRecords, label: 'Total de Registros' },
        { value: this.distinctJobs, label: 'Jobs Distintos' },
        { value: this.distinctResultados, label: 'Tipos de Resultado' },
        { value: this.filteredRecords, label: 'Registros Filtrados' }
      ];
    }
  }
};
</script>

<style scoped>
.stats-card {
  background: linear-gradient(135deg, #667eea 0%, #764ba2 100%);
  color: white;
  border-radius: 15px;
}
</style>