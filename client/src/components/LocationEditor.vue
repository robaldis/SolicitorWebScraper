<script setup lang="ts">
import { ref } from 'vue';

const props = defineProps<{ locations: string[] }>();
const emit = defineEmits<{ update: [locations: string[]] }>();

const newLocation = ref('');

function addLocation() {
  const trimmed = newLocation.value.trim();
  if (trimmed && !props.locations.includes(trimmed)) {
    emit('update', [...props.locations, trimmed]);
    newLocation.value = '';
  }
}

function removeLocation(location: string) {
  emit('update', props.locations.filter(l => l !== location));
}
</script>

<template>
  <div>
    <h3>Locations</h3>
    <ul>
      <li v-for="location in locations" :key="location">
        {{ location }}
        <button @click="removeLocation(location)">Remove</button>
      </li>
    </ul>
    <input v-model="newLocation" @keyup.enter="addLocation" placeholder="Add a location" />
    <button @click="addLocation">Add</button>
  </div>
</template>
