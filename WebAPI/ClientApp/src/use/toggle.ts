import { ref } from "vue";

export const useToggle = () => {
	const visible = ref<boolean>(false);

	const toggle = () => visible.value = !visible.value

	return { visible, toggle }
}