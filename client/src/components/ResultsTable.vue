<script setup lang="ts">
import type { Solicitor } from '../types/solicitor';

withDefaults(defineProps<{ solicitors: Solicitor[] }>(), {
  solicitors: () => [],
});
</script>

<template>
  <table v-if="solicitors.length">
    <thead>
      <tr>
        <th>Name</th><th>Location</th><th>Phone</th><th>Address</th><th>Reviews</th>
      </tr>
    </thead>
    <tbody>
      <tr v-for="(s, i) in solicitors" :key="i">
        <td>{{ s.name }}</td>
        <td>{{ s.location }}</td>
        <td>{{ s.phoneNumber ?? '—' }}</td>
        <td>{{ s.address ?? '—' }}</td>
        <td>{{ s.reviewCount ?? '—' }}</td>
      </tr>
    </tbody>
  </table>
  <p v-else>No results yet.</p>
</template>
