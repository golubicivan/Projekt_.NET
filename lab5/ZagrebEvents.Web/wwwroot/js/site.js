// ====================================================
// ZG Events — Klijentska skripta (Lab 4)
// ====================================================

(function () {
    'use strict';

    // ============= 1. FLATPICKR DATEPICKER =============
    // Automatska inicijalizacija svih input[data-include-time]
    function initDatePickers() {
        if (typeof flatpickr === 'undefined') return;

        document.querySelectorAll('.ze-datepicker').forEach(function (el) {
            if (el._flatpickr) return; // već inicijaliziran

            var includeTime = el.dataset.includeTime === 'true';
            var locale = el.dataset.locale || 'hr';
            var dateFormat = el.dataset.dateFormat || (includeTime ? 'd.m.Y H:i' : 'd.m.Y');

            flatpickr(el, {
                locale: locale === 'hr' ? 'hr' : 'default',
                enableTime: includeTime,
                time_24hr: locale === 'hr',
                dateFormat: dateFormat,
                allowInput: true,
                minuteIncrement: 15,
                position: 'auto'
            });
        });
    }

    // ============= 2. AUTOCOMPLETE DROPDOWN =============
    // Inicijalizacija svih .ze-autocomplete kontrola
    function initAutocomplete() {
        document.querySelectorAll('.ze-autocomplete').forEach(function (wrapper) {
            if (wrapper.dataset.initialized) return;
            wrapper.dataset.initialized = 'true';

            var input = wrapper.querySelector('.ze-autocomplete-input');
            var hidden = wrapper.querySelector('.ze-autocomplete-hidden');
            var dropdown = wrapper.querySelector('.ze-autocomplete-dropdown');
            var endpoint = wrapper.dataset.endpoint;
            var minChars = parseInt(wrapper.dataset.minChars || '1');

            var timer = null;

            input.addEventListener('input', function () {
                var query = input.value.trim();
                if (timer) clearTimeout(timer);

                if (query.length < minChars) {
                    dropdown.innerHTML = '';
                    dropdown.classList.remove('open');
                    return;
                }

                // Debounce 250ms da ne šaljemo zahtjev na svaki znak
                timer = setTimeout(function () {
                    fetchResults(query);
                }, 250);
            });

            input.addEventListener('focus', function () {
                if (input.value.trim().length >= minChars && dropdown.children.length > 0) {
                    dropdown.classList.add('open');
                }
            });

            // Klik izvan = zatvori
            document.addEventListener('click', function (e) {
                if (!wrapper.contains(e.target)) {
                    dropdown.classList.remove('open');
                }
            });

            function fetchResults(query) {
                dropdown.innerHTML = '<div class="ze-autocomplete-loading">⏳ Pretraga...</div>';
                dropdown.classList.add('open');

                fetch(endpoint + '?q=' + encodeURIComponent(query))
                    .then(function (r) { return r.json(); })
                    .then(function (results) {
                        dropdown.innerHTML = '';
                        if (!results || results.length === 0) {
                            dropdown.innerHTML = '<div class="ze-autocomplete-empty">Nema rezultata</div>';
                            return;
                        }
                        results.forEach(function (item) {
                            var div = document.createElement('div');
                            div.className = 'ze-autocomplete-item';
                            div.innerHTML = '<strong>' + escapeHtml(item.label) + '</strong>' +
                                (item.subtitle ? '<span class="ze-ac-subtitle">' + escapeHtml(item.subtitle) + '</span>' : '');
                            div.addEventListener('click', function () {
                                input.value = item.label;
                                hidden.value = item.id;
                                dropdown.classList.remove('open');
                                // Trigger blur validation
                                input.dispatchEvent(new Event('change', { bubbles: true }));
                            });
                            dropdown.appendChild(div);
                        });
                    })
                    .catch(function () {
                        dropdown.innerHTML = '<div class="ze-autocomplete-empty">Greška pri pretraživanju</div>';
                    });
            }
        });
    }

    function escapeHtml(text) {
        var div = document.createElement('div');
        div.textContent = text;
        return div.innerHTML;
    }

    // ============= 3. AJAX SEARCH na Index stranicama =============
    function initListSearch() {
        document.querySelectorAll('[data-ajax-search]').forEach(function (form) {
            if (form.dataset.initialized) return;
            form.dataset.initialized = 'true';

            var endpoint = form.dataset.ajaxSearch;
            var target = document.querySelector(form.dataset.target);
            if (!target) return;

            var timer = null;

            // Tekstualni inputi -> debounce; selecti -> odmah
            form.querySelectorAll('input[type="text"], input:not([type])').forEach(function (inp) {
                inp.addEventListener('input', function () {
                    if (timer) clearTimeout(timer);
                    timer = setTimeout(fetchList, 300);
                });
            });
            form.querySelectorAll('select').forEach(function (sel) {
                sel.addEventListener('change', fetchList);
            });

            form.addEventListener('submit', function (e) {
                e.preventDefault();
                fetchList();
            });

            function fetchList() {
                // Skupi sve form vrijednosti (q, type, status, minRating...)
                var params = new URLSearchParams();
                form.querySelectorAll('input, select').forEach(function (el) {
                    if (!el.name) return;
                    if ((el.type === 'checkbox' || el.type === 'radio') && !el.checked) return;
                    if (el.value !== '') params.append(el.name, el.value);
                });
                showSpinner(target);
                fetch(endpoint + '?' + params.toString())
                    .then(function (r) { return r.text(); })
                    .then(function (html) {
                        target.innerHTML = html;
                        initDatePickers();
                        initAutocomplete();
                    });
            }
        });
    }

    // ============= 4. LOADING SPINNER =============
    function showSpinner(target) {
        target.innerHTML = '<div class="ze-spinner-wrap"><div class="ze-spinner"></div><div class="ze-spinner-text">Učitavam...</div></div>';
    }

    // Globalni spinner za forme i AJAX
    window.zeSpinner = {
        show: function (text) {
            var el = document.getElementById('ze-global-spinner');
            if (!el) {
                el = document.createElement('div');
                el.id = 'ze-global-spinner';
                el.innerHTML = '<div class="ze-spinner"></div><div>' + (text || 'Učitavam...') + '</div>';
                document.body.appendChild(el);
            }
            el.style.display = 'flex';
        },
        hide: function () {
            var el = document.getElementById('ze-global-spinner');
            if (el) el.style.display = 'none';
        }
    };

    // ============= 5. SMOOTH SCROLL =============
    document.addEventListener('click', function (e) {
        var a = e.target.closest('a[href^="#"]');
        if (!a) return;
        var href = a.getAttribute('href');
        if (href.length <= 1) return;
        var target = document.querySelector(href);
        if (!target) return;
        e.preventDefault();
        target.scrollIntoView({ behavior: 'smooth', block: 'start' });
    });

    // ============= 6. DELETE CONFIRMATION MODAL =============
    function initDeleteConfirm() {
        document.querySelectorAll('form[data-confirm-delete]').forEach(function (form) {
            if (form.dataset.confirmInit) return;
            form.dataset.confirmInit = 'true';
            form.addEventListener('submit', function (e) {
                var msg = form.dataset.confirmDelete || 'Sigurno želiš obrisati ovaj zapis?';
                if (!confirm(msg)) {
                    e.preventDefault();
                }
            });
        });
    }

    // ============= 7. AUTO-SUBMIT TOAST =============
    function initToasts() {
        var toasts = document.querySelectorAll('.ze-toast');
        toasts.forEach(function (t) {
            setTimeout(function () {
                t.classList.add('ze-toast-out');
                setTimeout(function () { t.remove(); }, 400);
            }, 4000);
        });
    }

    // ============= INIT ALL on DOM ready =============
    document.addEventListener('DOMContentLoaded', function () {
        initDatePickers();
        initAutocomplete();
        initListSearch();
        initDeleteConfirm();
        initToasts();
    });

    // Expose za re-init nakon AJAX-a
    window.zeInit = function () {
        initDatePickers();
        initAutocomplete();
        initDeleteConfirm();
        initToasts();
    };
})();
