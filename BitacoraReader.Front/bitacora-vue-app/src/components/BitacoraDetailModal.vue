<template>
  <div class="modal fade" id="detailModal" tabindex="-1" aria-labelledby="detailModalLabel" aria-hidden="true">
    <div class="modal-dialog modal-lg">
      <div class="modal-content">
        <div class="modal-header">
          <h5 class="modal-title" id="detailModalLabel">
            <i class="bi bi-info-circle me-2"></i>
            Detalle de Bitácora
          </h5>
          <button type="button" class="btn-close" @click="closeModal" aria-label="Close"></button>
        </div>
        <div class="modal-body" v-if="bitacora">
          <div class="row">
            <div class="col-md-6">
              <p><strong>ID Bitácora:</strong> {{ bitacora.idBitacora }}</p>
              <p><strong>ID Job:</strong> {{ bitacora.idJob }}</p>
              <p><strong>Día:</strong> {{ bitacora.dia }}</p>
              <p><strong>Hora:</strong> {{ formatTime(bitacora.hora) }}</p>
              <p><strong>Archivo:</strong> {{ bitacora.nombreArchivo }}</p>
            </div>
            <div class="col-md-6">
              <p><strong>Fecha Inicio:</strong> {{ formatDateTime(bitacora.fechaInicio) }}</p>
              <p><strong>Fecha Término:</strong> {{ formatDateTime(bitacora.fechaTermino) }}</p>
              <p><strong>Duración:</strong> {{ bitacora.duracionFormateada || 'N/A' }}</p>
              <p><strong>Estado:</strong> 
                <span class="badge" :class="getStatusClass(bitacora)">
                  {{ getStatusText(bitacora) }}
                </span>
              </p>
            </div>
          </div>
          <div class="row">
            <div class="col-12">
              <p><strong>Resultado:</strong></p>
              <div class="border p-3 bg-light rounded">
                {{ bitacora.resultado }}
              </div>
            </div>
          </div>
        </div>
        <div class="modal-footer">
          <button type="button" class="btn btn-secondary" @click="closeModal">Cerrar</button>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  props: {
    bitacora: {
      type: Object,
      required: true
    }
  },
  methods: {
    closeModal() {
      this.$emit('close');
    },
    formatDateTime(dateString) {
      if (!dateString) return 'N/A';
      return new Date(dateString).toLocaleString('es-ES');
    },
    formatTime(timeString) {
      if (!timeString) return 'N/A';
      return timeString;
    },
    getStatusClass(bitacora) {
      if (!bitacora.estaCompleto) return 'bg-warning';
      return bitacora.esExitoso ? 'bg-success' : 'bg-danger';
    },
    getStatusText(bitacora) {
      if (!bitacora.estaCompleto) return 'En Proceso';
      return bitacora.esExitoso ? 'Exitoso' : 'Error';
    }
  }
}
</script>

<style scoped>
.modal-header {
  background: linear-gradient(135deg, #667eea 0%, #764ba2 100%);
  color: white;
}
</style>