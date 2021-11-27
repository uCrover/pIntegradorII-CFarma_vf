const formulario = document.getElementById('formulario');
const inputs = document.querySelectorAll('#formulario input');

const expresiones = {
	usuario: /^[a-zA-Z0-9\_\-]{4,16}$/, // Letras, numeros, guion y guion_bajo
	nombre: /^[a-zA-ZÀ-ÿ\s]{4,16}$/, // Letras y espacios, pueden llevar acentos.
	apellido: /^[a-zA-ZÀ-ÿ\s]{1,50}$/, // Letras y espacios, pueden llevar acentos.
	password: /^.{4,12}$/, // 4 a 12 digitos.
	correo: /^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$/,
	telefono: /([9]{1}[0-9]{8})$/, // 7 a 14 numeros.
	dni: /^([0-9]){8,8}$/
};

const campos = {
	nombre: false,
	apellido: false,
	nrodoc: false,
	movil: false,
	correo: false,
	pswd: false
};





const validarFormulario = (e) => {
	switch (e.target.name) {
		case "nombre":
			validarCampo(expresiones.nombre, e.target, 'nombre');
		break;

		case "apellido":
			validarCampo(expresiones.apellido, e.target, 'apellido');
			break;

		case "nrodoc":
			validarCampo(expresiones.dni, e.target, 'nrodoc');
		break;

		case "movil":
			validarCampo(expresiones.telefono, e.target, 'movil');
		break;

		case "correo":
			validarCampo(expresiones.correo, e.target, 'correo')
		break;

		case "pswd":
			validarCampo(expresiones.password, e.target, 'pswd')
		break;

		case "pswd2":
			validarPswd2();
		break;				
    }	

};



const validarCampo = (expresion, input, campo) => {
	if (expresion.test(input.value)) {
		document.getElementById(`grupo__${campo}`).classList.remove('formulario__grupo-incorrecto');
		document.getElementById(`grupo__${campo}`).classList.add('formulario__grupo-correcto');
		document.querySelector(`#grupo__${campo} i`).classList.add('fa-check-circle');
		document.querySelector(`#grupo__${campo} i`).classList.remove('fa-times-circle');
		document.querySelector(`#grupo__${campo} .formulario__input-error`).classList.remove('formulario__input-error-activo');
		campos[campo] = true;
	} else {
		document.getElementById(`grupo__${campo}`).classList.add('formulario__grupo-incorrecto');
		document.getElementById(`grupo__${campo}`).classList.remove('formulario__grupo-correcto');
		document.querySelector(`#grupo__${campo} i`).classList.add('fa-times-circle');
		document.querySelector(`#grupo__${campo} i`).classList.remove('fa-check-circle');
		document.querySelector(`#grupo__${campo} .formulario__input-error`).classList.add('formulario__input-error-activo');
		campos[campo] = false;
	}
};

const validarPswd2 = () => {
	const inputPassword1 = document.getElementById('pswd');
	const inputPassword2 = document.getElementById('pswd2');

	if (inputPassword1.value !== inputPassword2.value) {
		document.getElementById(`grupo__pswd2`).classList.add('formulario__grupo-incorrecto');
		document.getElementById(`grupo__pswd2`).classList.remove('formulario__grupo-correcto');
		document.querySelector(`#grupo__pswd2 i`).classList.add('fa-times-circle');
		document.querySelector(`#grupo__pswd2 i`).classList.remove('fa-check-circle');
		document.querySelector(`#grupo__pswd2 .formulario__input-error`).classList.add('formulario__input-error-activo');
		campos['pswd'] = false;

	} else {
		document.getElementById(`grupo__pswd2`).classList.remove('formulario__grupo-incorrecto');
		document.getElementById(`grupo__pswd2`).classList.add('formulario__grupo-correcto');
		document.querySelector(`#grupo__pswd2 i`).classList.remove('fa-times-circle');
		document.querySelector(`#grupo__pswd2 i`).classList.add('fa-check-circle');
		document.querySelector(`#grupo__pswd2 .formulario__input-error`).classList.remove('formulario__input-error-activo');
		campos['pswd'] = true;

	}

};
inputs.forEach((input) => {
	input.addEventListener('keyup', validarFormulario);
	input.addEventListener('blur', validarFormulario);
});

formulario.addEventListener('submit', (e) => {
	e.preventDefault();

	const terminos = document.getElementById('terminos');

	if (campos.nombre && campos.apellido && campos.correo && campos.movil &&
		campos.nrodoc && campos.pswd && terminos.checked) {
		formulario.reset();
		
		alert("Registrado correctamente")
		document.getElementById('formulario__mensaje-exito').classList.add('formulario__mensaje-exito-activo');
		setTimeout(() => {
			document.getElementById('formulario__mensaje-exito').classList.remove('formulario__mensaje-exito-activo');

		}, 5000);

		document.querySelectorAll('.formulario__grupo-correcto').forEach((icono) => {
			icono.classList.remove('formulario__grupo-correcto');
		});

	} else {
		document.getElementById('formulario__mensaje').classList.add('formulario__mensaje-activo');
    }
});

