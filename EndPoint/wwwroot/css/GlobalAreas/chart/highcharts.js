/*
 Highcharts JS v5.0.6 (2016-12-07)

 (c) 2009-2016 Torstein Honsi

 License: www.highcharts.com/license
*/
(function (L, a) { "object" === typeof module && module.exports ? module.exports = L.document ? a(L) : a : L.Highcharts = a(L) })("undefined" !== typeof window ? window : this, function (L) {
    L = function () {
        var a = window, D = a.document, C = a.navigator && a.navigator.userAgent || "", G = D && D.createElementNS && !!D.createElementNS("http://www.w3.org/2000/svg", "svg").createSVGRect, I = /(edge|msie|trident)/i.test(C) && !window.opera, h = !G, f = /Firefox/.test(C), p = f && 4 > parseInt(C.split("Firefox/")[1], 10); return a.Highcharts ? a.Highcharts.error(16, !0) : {
            product: "Highcharts",
            version: "5.0.6", deg2rad: 2 * Math.PI / 360, doc: D, hasBidiBug: p, hasTouch: D && void 0 !== D.documentElement.ontouchstart, isMS: I, isWebKit: /AppleWebKit/.test(C), isFirefox: f, isTouchDevice: /(Mobile|Android|Windows Phone)/.test(C), SVG_NS: "http://www.w3.org/2000/svg", chartCount: 0, seriesTypes: {}, symbolSizes: {}, svg: G, vml: h, win: a, charts: [], marginNames: ["plotTop", "marginRight", "marginBottom", "plotLeft"], noop: function () { }
        }
    }(); (function (a) {
        var D = [], C = a.charts, G = a.doc, I = a.win; a.error = function (h, f) {
            h = a.isNumber(h) ? "Highcharts error #" +
                h + ": www.highcharts.com/errors/" + h : h; if (f) throw Error(h);
                I.console && console.log(h)
        }; a.Fx = function (a, f, p) { this.options = f; this.elem = a; this.prop = p }; a.Fx.prototype = {
            dSetter: function () { var a = this.paths[0], f = this.paths[1], p = [], v = this.now, l = a.length, u; if (1 === v) p = this.toD; else if (l === f.length && 1 > v) for (; l--;)u = parseFloat(a[l]), p[l] = isNaN(u) ? a[l] : v * parseFloat(f[l] - u) + u; else p = f; this.elem.attr("d", p, null, !0) }, update: function () {
                var a = this.elem, f = this.prop, p = this.now, v = this.options.step; if (this[f + "Setter"]) this[f +
                    "Setter"](); else a.attr ? a.element && a.attr(f, p, null, !0) : a.style[f] = p + this.unit; v && v.call(a, p, this)
            }, run: function (a, f, p) { var h = this, l = function (a) { return l.stopped ? !1 : h.step(a) }, u; this.startTime = +new Date; this.start = a; this.end = f; this.unit = p; this.now = this.start; this.pos = 0; l.elem = this.elem; l.prop = this.prop; l() && 1 === D.push(l) && (l.timerId = setInterval(function () { for (u = 0; u < D.length; u++)D[u]() || D.splice(u--, 1); D.length || clearInterval(l.timerId) }, 13)) }, step: function (a) {
                var f = +new Date, h, v = this.options; h = this.elem;
                var l = v.complete, u = v.duration, d = v.curAnim, c; if (h.attr && !h.element) h = !1; else if (a || f >= u + this.startTime) { this.now = this.end; this.pos = 1; this.update(); a = d[this.prop] = !0; for (c in d) !0 !== d[c] && (a = !1); a && l && l.call(h); h = !1 } else this.pos = v.easing((f - this.startTime) / u), this.now = this.start + (this.end - this.start) * this.pos, this.update(), h = !0; return h
            }, initPath: function (h, f, p) {
                function v(a) {
                    var e, b; for (q = a.length; q--;)e = "M" === a[q] || "L" === a[q], b = /[a-zA-Z]/.test(a[q + 3]), e && b && a.splice(q + 1, 0, a[q + 1], a[q + 2], a[q + 1], a[q +
                        2])
                } function l(a, e) { for (; a.length < m;) { a[0] = e[m - a.length]; var b = a.slice(0, t);[].splice.apply(a, [0, 0].concat(b)); z && (b = a.slice(a.length - t), [].splice.apply(a, [a.length, 0].concat(b)), q--) } a[0] = "M" } function u(a, e) { for (var c = (m - a.length) / t; 0 < c && c--;)b = a.slice().splice(a.length / F - t, t * F), b[0] = e[m - t - c * t], y && (b[t - 6] = b[t - 2], b[t - 5] = b[t - 1]), [].splice.apply(a, [a.length / F, 0].concat(b)), z && c-- } f = f || ""; var d, c = h.startX, n = h.endX, y = -1 < f.indexOf("C"), t = y ? 7 : 3, m, b, q; f = f.split(" "); p = p.slice(); var z = h.isArea, F = z ? 2 : 1, e;
                y && (v(f), v(p)); if (c && n) { for (q = 0; q < c.length; q++)if (c[q] === n[0]) { d = q; break } else if (c[0] === n[n.length - c.length + q]) { d = q; e = !0; break } void 0 === d && (f = []) } f.length && a.isNumber(d) && (m = p.length + d * F * t, e ? (l(f, p), u(p, f)) : (l(p, f), u(f, p))); return [f, p]
            }
        }; a.extend = function (a, f) { var h; a || (a = {}); for (h in f) a[h] = f[h]; return a }; a.merge = function () {
            var h, f = arguments, p, v = {}, l = function (h, d) {
                var c, n; "object" !== typeof h && (h = {}); for (n in d) d.hasOwnProperty(n) && (c = d[n], a.isObject(c, !0) && "renderTo" !== n && "number" !== typeof c.nodeType ?
                    h[n] = l(h[n] || {}, c) : h[n] = d[n]); return h
            }; !0 === f[0] && (v = f[1], f = Array.prototype.slice.call(f, 2)); p = f.length; for (h = 0; h < p; h++)v = l(v, f[h]); return v
        }; a.pInt = function (a, f) { return parseInt(a, f || 10) }; a.isString = function (a) { return "string" === typeof a }; a.isArray = function (a) { a = Object.prototype.toString.call(a); return "[object Array]" === a || "[object Array Iterator]" === a }; a.isObject = function (h, f) { return h && "object" === typeof h && (!f || !a.isArray(h)) }; a.isNumber = function (a) { return "number" === typeof a && !isNaN(a) }; a.erase =
            function (a, f) { for (var h = a.length; h--;)if (a[h] === f) { a.splice(h, 1); break } }; a.defined = function (a) { return void 0 !== a && null !== a }; a.attr = function (h, f, p) { var v, l; if (a.isString(f)) a.defined(p) ? h.setAttribute(f, p) : h && h.getAttribute && (l = h.getAttribute(f)); else if (a.defined(f) && a.isObject(f)) for (v in f) h.setAttribute(v, f[v]); return l }; a.splat = function (h) { return a.isArray(h) ? h : [h] }; a.syncTimeout = function (a, f, p) { if (f) return setTimeout(a, f, p); a.call(0, p) }; a.pick = function () {
                var a = arguments, f, p, v = a.length; for (f =
                    0; f < v; f++)if (p = a[f], void 0 !== p && null !== p) return p
            }; a.css = function (h, f) { a.isMS && !a.svg && f && void 0 !== f.opacity && (f.filter = "alpha(opacity\x3d" + 100 * f.opacity + ")"); a.extend(h.style, f) }; a.createElement = function (h, f, p, v, l) { h = G.createElement(h); var u = a.css; f && a.extend(h, f); l && u(h, { padding: 0, border: "none", margin: 0 }); p && u(h, p); v && v.appendChild(h); return h }; a.extendClass = function (h, f) { var p = function () { }; p.prototype = new h; a.extend(p.prototype, f); return p }; a.pad = function (a, f, p) {
                return Array((f || 2) + 1 - String(a).length).join(p ||
                    0) + a
            }; a.relativeLength = function (a, f) { return /%$/.test(a) ? f * parseFloat(a) / 100 : parseFloat(a) }; a.wrap = function (a, f, p) { var h = a[f]; a[f] = function () { var a = Array.prototype.slice.call(arguments), f = arguments, d = this; d.proceed = function () { h.apply(d, arguments.length ? arguments : f) }; a.unshift(h); a = p.apply(this, a); d.proceed = null; return a } }; a.getTZOffset = function (h) { var f = a.Date; return 6E4 * (f.hcGetTimezoneOffset && f.hcGetTimezoneOffset(h) || f.hcTimezoneOffset || 0) }; a.dateFormat = function (h, f, p) {
                if (!a.defined(f) || isNaN(f)) return a.defaultOptions.lang.invalidDate ||
                    ""; h = a.pick(h, "%Y-%m-%d %H:%M:%S"); var v = a.Date, l = new v(f - a.getTZOffset(f)), u, d = l[v.hcGetHours](), c = l[v.hcGetDay](), n = l[v.hcGetDate](), y = l[v.hcGetMonth](), t = l[v.hcGetFullYear](), m = a.defaultOptions.lang, b = m.weekdays, q = m.shortWeekdays, z = a.pad, v = a.extend({
                        a: q ? q[c] : b[c].substr(0, 3), A: b[c], d: z(n), e: z(n, 2, " "), w: c, b: m.shortMonths[y], B: m.months[y], m: z(y + 1), y: t.toString().substr(2, 2), Y: t, H: z(d), k: d, I: z(d % 12 || 12), l: d % 12 || 12, M: z(l[v.hcGetMinutes]()), p: 12 > d ? "AM" : "PM", P: 12 > d ? "am" : "pm", S: z(l.getSeconds()), L: z(Math.round(f %
                            1E3), 3)
                    }, a.dateFormats); for (u in v) for (; -1 !== h.indexOf("%" + u);)h = h.replace("%" + u, "function" === typeof v[u] ? v[u](f) : v[u]); return p ? h.substr(0, 1).toUpperCase() + h.substr(1) : h
            }; a.formatSingle = function (h, f) { var p = /\.([0-9])/, v = a.defaultOptions.lang; /f$/.test(h) ? (p = (p = h.match(p)) ? p[1] : -1, null !== f && (f = a.numberFormat(f, p, v.decimalPoint, -1 < h.indexOf(",") ? v.thousandsSep : ""))) : f = a.dateFormat(h, f); return f }; a.format = function (h, f) {
                for (var p = "{", v = !1, l, u, d, c, n = [], y; h;) {
                    p = h.indexOf(p); if (-1 === p) break; l = h.slice(0,
                        p); if (v) { l = l.split(":"); u = l.shift().split("."); c = u.length; y = f; for (d = 0; d < c; d++)y = y[u[d]]; l.length && (y = a.formatSingle(l.join(":"), y)); n.push(y) } else n.push(l); h = h.slice(p + 1); p = (v = !v) ? "}" : "{"
                } n.push(h); return n.join("")
            }; a.getMagnitude = function (a) { return Math.pow(10, Math.floor(Math.log(a) / Math.LN10)) }; a.normalizeTickInterval = function (h, f, p, v, l) {
                var u, d = h; p = a.pick(p, 1); u = h / p; f || (f = l ? [1, 1.2, 1.5, 2, 2.5, 3, 4, 5, 6, 8, 10] : [1, 2, 2.5, 5, 10], !1 === v && (1 === p ? f = a.grep(f, function (a) { return 0 === a % 1 }) : .1 >= p && (f = [1 / p])));
                for (v = 0; v < f.length && !(d = f[v], l && d * p >= h || !l && u <= (f[v] + (f[v + 1] || f[v])) / 2); v++); return d * p
            }; a.stableSort = function (a, f) { var p = a.length, h, l; for (l = 0; l < p; l++)a[l].safeI = l; a.sort(function (a, d) { h = f(a, d); return 0 === h ? a.safeI - d.safeI : h }); for (l = 0; l < p; l++)delete a[l].safeI }; a.arrayMin = function (a) { for (var f = a.length, p = a[0]; f--;)a[f] < p && (p = a[f]); return p }; a.arrayMax = function (a) { for (var f = a.length, p = a[0]; f--;)a[f] > p && (p = a[f]); return p }; a.destroyObjectProperties = function (a, f) {
                for (var p in a) a[p] && a[p] !== f && a[p].destroy &&
                    a[p].destroy(), delete a[p]
            }; a.discardElement = function (h) { var f = a.garbageBin; f || (f = a.createElement("div")); h && f.appendChild(h); f.innerHTML = "" }; a.correctFloat = function (a, f) { return parseFloat(a.toPrecision(f || 14)) }; a.setAnimation = function (h, f) { f.renderer.globalAnimation = a.pick(h, f.options.chart.animation, !0) }; a.animObject = function (h) { return a.isObject(h) ? a.merge(h) : { duration: h ? 500 : 0 } }; a.timeUnits = { millisecond: 1, second: 1E3, minute: 6E4, hour: 36E5, day: 864E5, week: 6048E5, month: 24192E5, year: 314496E5 }; a.numberFormat =
                function (h, f, p, v) { h = +h || 0; f = +f; var l = a.defaultOptions.lang, u = (h.toString().split(".")[1] || "").length, d, c, n = Math.abs(h); -1 === f ? f = Math.min(u, 20) : a.isNumber(f) || (f = 2); d = String(a.pInt(n.toFixed(f))); c = 3 < d.length ? d.length % 3 : 0; p = a.pick(p, l.decimalPoint); v = a.pick(v, l.thousandsSep); h = (0 > h ? "-" : "") + (c ? d.substr(0, c) + v : ""); h += d.substr(c).replace(/(\d{3})(?=\d)/g, "$1" + v); f && (v = Math.abs(n - d + Math.pow(10, -Math.max(f, u) - 1)), h += p + v.toFixed(f).slice(2)); return h }; Math.easeInOutSine = function (a) {
                    return -.5 * (Math.cos(Math.PI *
                        a) - 1)
                }; a.getStyle = function (h, f) { return "width" === f ? Math.min(h.offsetWidth, h.scrollWidth) - a.getStyle(h, "padding-left") - a.getStyle(h, "padding-right") : "height" === f ? Math.min(h.offsetHeight, h.scrollHeight) - a.getStyle(h, "padding-top") - a.getStyle(h, "padding-bottom") : (h = I.getComputedStyle(h, void 0)) && a.pInt(h.getPropertyValue(f)) }; a.inArray = function (a, f) { return f.indexOf ? f.indexOf(a) : [].indexOf.call(f, a) }; a.grep = function (a, f) { return [].filter.call(a, f) }; a.find = function (a, f) { return [].find.call(a, f) }; a.map = function (a,
                    f) { for (var p = [], h = 0, l = a.length; h < l; h++)p[h] = f.call(a[h], a[h], h, a); return p }; a.offset = function (a) { var f = G.documentElement; a = a.getBoundingClientRect(); return { top: a.top + (I.pageYOffset || f.scrollTop) - (f.clientTop || 0), left: a.left + (I.pageXOffset || f.scrollLeft) - (f.clientLeft || 0) } }; a.stop = function (a, f) { for (var p = D.length; p--;)D[p].elem !== a || f && f !== D[p].prop || (D[p].stopped = !0) }; a.each = function (a, f, p) { return Array.prototype.forEach.call(a, f, p) }; a.addEvent = function (h, f, p) {
                        function v(a) {
                        a.target = a.srcElement ||
                            I; p.call(h, a)
                        } var l = h.hcEvents = h.hcEvents || {}; h.addEventListener ? h.addEventListener(f, p, !1) : h.attachEvent && (h.hcEventsIE || (h.hcEventsIE = {}), h.hcEventsIE[p.toString()] = v, h.attachEvent("on" + f, v)); l[f] || (l[f] = []); l[f].push(p); return function () { a.removeEvent(h, f, p) }
                    }; a.removeEvent = function (h, f, p) {
                        function v(a, c) { h.removeEventListener ? h.removeEventListener(a, c, !1) : h.attachEvent && (c = h.hcEventsIE[c.toString()], h.detachEvent("on" + a, c)) } function l() {
                            var a, c; if (h.nodeName) for (c in f ? (a = {}, a[f] = !0) : a = d, a) if (d[c]) for (a =
                                d[c].length; a--;)v(c, d[c][a])
                        } var u, d = h.hcEvents, c; d && (f ? (u = d[f] || [], p ? (c = a.inArray(p, u), -1 < c && (u.splice(c, 1), d[f] = u), v(f, p)) : (l(), d[f] = [])) : (l(), h.hcEvents = {}))
                    }; a.fireEvent = function (h, f, p, v) {
                        var l; l = h.hcEvents; var u, d; p = p || {}; if (G.createEvent && (h.dispatchEvent || h.fireEvent)) l = G.createEvent("Events"), l.initEvent(f, !0, !0), a.extend(l, p), h.dispatchEvent ? h.dispatchEvent(l) : h.fireEvent(f, l); else if (l) for (l = l[f] || [], u = l.length, p.target || a.extend(p, {
                            preventDefault: function () { p.defaultPrevented = !0 }, target: h,
                            type: f
                        }), f = 0; f < u; f++)(d = l[f]) && !1 === d.call(h, p) && p.preventDefault(); v && !p.defaultPrevented && v(p)
                    }; a.animate = function (h, f, p) {
                        var v, l = "", u, d, c; a.isObject(p) || (v = arguments, p = { duration: v[2], easing: v[3], complete: v[4] }); a.isNumber(p.duration) || (p.duration = 400); p.easing = "function" === typeof p.easing ? p.easing : Math[p.easing] || Math.easeInOutSine; p.curAnim = a.merge(f); for (c in f) a.stop(h, c), d = new a.Fx(h, p, c), u = null, "d" === c ? (d.paths = d.initPath(h, h.d, f.d), d.toD = f.d, v = 0, u = 1) : h.attr ? v = h.attr(c) : (v = parseFloat(a.getStyle(h,
                            c)) || 0, "opacity" !== c && (l = "px")), u || (u = f[c]), u.match && u.match("px") && (u = u.replace(/px/g, "")), d.run(v, u, l)
                    }; a.seriesType = function (h, f, p, v, l) { var u = a.getOptions(), d = a.seriesTypes; u.plotOptions[h] = a.merge(u.plotOptions[f], p); d[h] = a.extendClass(d[f] || function () { }, v); d[h].prototype.type = h; l && (d[h].prototype.pointClass = a.extendClass(a.Point, l)); return d[h] }; a.uniqueKey = function () { var a = Math.random().toString(36).substring(2, 9), f = 0; return function () { return "highcharts-" + a + "-" + f++ } }(); I.jQuery && (I.jQuery.fn.highcharts =
                        function () { var h = [].slice.call(arguments); if (this[0]) return h[0] ? (new (a[a.isString(h[0]) ? h.shift() : "Chart"])(this[0], h[0], h[1]), this) : C[a.attr(this[0], "data-highcharts-chart")] }); G && !G.defaultView && (a.getStyle = function (h, f) {
                            var p = { width: "clientWidth", height: "clientHeight" }[f]; if (h.style[f]) return a.pInt(h.style[f]); "opacity" === f && (f = "filter"); if (p) return h.style.zoom = 1, Math.max(h[p] - 2 * a.getStyle(h, "padding"), 0); h = h.currentStyle[f.replace(/\-(\w)/g, function (a, l) { return l.toUpperCase() })]; "filter" ===
                                f && (h = h.replace(/alpha\(opacity=([0-9]+)\)/, function (a, l) { return l / 100 })); return "" === h ? 1 : a.pInt(h)
                        }); Array.prototype.forEach || (a.each = function (a, f, p) { for (var h = 0, l = a.length; h < l; h++)if (!1 === f.call(p, a[h], h, a)) return h }); Array.prototype.indexOf || (a.inArray = function (a, f) { var p, h = 0; if (f) for (p = f.length; h < p; h++)if (f[h] === a) return h; return -1 }); Array.prototype.filter || (a.grep = function (a, f) { for (var p = [], h = 0, l = a.length; h < l; h++)f(a[h], h) && p.push(a[h]); return p }); Array.prototype.find || (a.find = function (a, f) {
                            var p,
                            h = a.length; for (p = 0; p < h; p++)if (f(a[p], p)) return a[p]
                        })
    })(L); (function (a) {
        var D = a.each, C = a.isNumber, G = a.map, I = a.merge, h = a.pInt; a.Color = function (f) { if (!(this instanceof a.Color)) return new a.Color(f); this.init(f) }; a.Color.prototype = {
            parsers: [{ regex: /rgba\(\s*([0-9]{1,3})\s*,\s*([0-9]{1,3})\s*,\s*([0-9]{1,3})\s*,\s*([0-9]?(?:\.[0-9]+)?)\s*\)/, parse: function (a) { return [h(a[1]), h(a[2]), h(a[3]), parseFloat(a[4], 10)] } }, {
                regex: /#([a-fA-F0-9]{2})([a-fA-F0-9]{2})([a-fA-F0-9]{2})/, parse: function (a) {
                    return [h(a[1],
                        16), h(a[2], 16), h(a[3], 16), 1]
                }
            }, { regex: /rgb\(\s*([0-9]{1,3})\s*,\s*([0-9]{1,3})\s*,\s*([0-9]{1,3})\s*\)/, parse: function (a) { return [h(a[1]), h(a[2]), h(a[3]), 1] } }], names: { white: "#ffffff", black: "#000000" }, init: function (f) { var p, h, l, u; if ((this.input = f = this.names[f] || f) && f.stops) this.stops = G(f.stops, function (d) { return new a.Color(d[1]) }); else for (l = this.parsers.length; l-- && !h;)u = this.parsers[l], (p = u.regex.exec(f)) && (h = u.parse(p)); this.rgba = h || [] }, get: function (a) {
                var f = this.input, h = this.rgba, l; this.stops ?
                    (l = I(f), l.stops = [].concat(l.stops), D(this.stops, function (f, d) { l.stops[d] = [l.stops[d][0], f.get(a)] })) : l = h && C(h[0]) ? "rgb" === a || !a && 1 === h[3] ? "rgb(" + h[0] + "," + h[1] + "," + h[2] + ")" : "a" === a ? h[3] : "rgba(" + h.join(",") + ")" : f; return l
            }, brighten: function (a) { var f, v = this.rgba; if (this.stops) D(this.stops, function (l) { l.brighten(a) }); else if (C(a) && 0 !== a) for (f = 0; 3 > f; f++)v[f] += h(255 * a), 0 > v[f] && (v[f] = 0), 255 < v[f] && (v[f] = 255); return this }, setOpacity: function (a) { this.rgba[3] = a; return this }
        }; a.color = function (f) { return new a.Color(f) }
    })(L);
    (function (a) {
        var D, C, G = a.addEvent, I = a.animate, h = a.attr, f = a.charts, p = a.color, v = a.css, l = a.createElement, u = a.defined, d = a.deg2rad, c = a.destroyObjectProperties, n = a.doc, y = a.each, t = a.extend, m = a.erase, b = a.grep, q = a.hasTouch, z = a.isArray, F = a.isFirefox, e = a.isMS, r = a.isObject, x = a.isString, A = a.isWebKit, k = a.merge, w = a.noop, K = a.pick, J = a.pInt, N = a.removeEvent, g = a.stop, B = a.svg, S = a.SVG_NS, M = a.symbolSizes, R = a.win; D = a.SVGElement = function () { return this }; D.prototype = {
            opacity: 1, SVG_NS: S, textProps: "direction fontSize fontWeight fontFamily fontStyle color lineHeight width textDecoration textOverflow textOutline".split(" "),
            init: function (a, H) { this.element = "span" === H ? l(H) : n.createElementNS(this.SVG_NS, H); this.renderer = a }, animate: function (E, H, g) { H = a.animObject(K(H, this.renderer.globalAnimation, !0)); 0 !== H.duration ? (g && (H.complete = g), I(this, E, H)) : this.attr(E, null, g); return this }, colorGradient: function (E, H, g) {
                var e = this.renderer, c, b, B, r, m, w, q, d, x, n, P, t = [], A; E.linearGradient ? b = "linearGradient" : E.radialGradient && (b = "radialGradient"); if (b) {
                    B = E[b]; m = e.gradients; q = E.stops; n = g.radialReference; z(B) && (E[b] = B = {
                        x1: B[0], y1: B[1], x2: B[2],
                        y2: B[3], gradientUnits: "userSpaceOnUse"
                    }); "radialGradient" === b && n && !u(B.gradientUnits) && (r = B, B = k(B, e.getRadialAttr(n, r), { gradientUnits: "userSpaceOnUse" })); for (P in B) "id" !== P && t.push(P, B[P]); for (P in q) t.push(q[P]); t = t.join(","); m[t] ? n = m[t].attr("id") : (B.id = n = a.uniqueKey(), m[t] = w = e.createElement(b).attr(B).add(e.defs), w.radAttr = r, w.stops = [], y(q, function (E) {
                        0 === E[1].indexOf("rgba") ? (c = a.color(E[1]), d = c.get("rgb"), x = c.get("a")) : (d = E[1], x = 1); E = e.createElement("stop").attr({
                            offset: E[0], "stop-color": d,
                            "stop-opacity": x
                        }).add(w); w.stops.push(E)
                    })); A = "url(" + e.url + "#" + n + ")"; g.setAttribute(H, A); g.gradient = t; E.toString = function () { return A }
                }
            }, applyTextOutline: function (a) {
                var E = this.element, g, e, k, b; -1 !== a.indexOf("contrast") && (a = a.replace(/contrast/g, this.renderer.getContrast(E.style.fill))); this.fakeTS = !0; this.ySetter = this.xSetter; g = [].slice.call(E.getElementsByTagName("tspan")); a = a.split(" "); e = a[a.length - 1]; (k = a[0]) && "none" !== k && (k = k.replace(/(^[\d\.]+)(.*?)$/g, function (a, E, H) { return 2 * E + H }), y(g, function (a) {
                "highcharts-text-outline" ===
                    a.getAttribute("class") && m(g, E.removeChild(a))
                }), b = E.firstChild, y(g, function (a, H) { 0 === H && (a.setAttribute("x", E.getAttribute("x")), H = E.getAttribute("y"), a.setAttribute("y", H || 0), null === H && E.setAttribute("y", 0)); a = a.cloneNode(1); h(a, { "class": "highcharts-text-outline", fill: e, stroke: e, "stroke-width": k, "stroke-linejoin": "round" }); E.insertBefore(a, b) }))
            }, attr: function (a, H, e, k) {
                var E, b = this.element, c, B = this, r; "string" === typeof a && void 0 !== H && (E = a, a = {}, a[E] = H); if ("string" === typeof a) B = (this[a + "Getter"] ||
                    this._defaultGetter).call(this, a, b); else { for (E in a) H = a[E], r = !1, k || g(this, E), this.symbolName && /^(x|y|width|height|r|start|end|innerR|anchorX|anchorY)/.test(E) && (c || (this.symbolAttr(a), c = !0), r = !0), !this.rotation || "x" !== E && "y" !== E || (this.doTransform = !0), r || (r = this[E + "Setter"] || this._defaultSetter, r.call(this, H, E, b), this.shadows && /^(width|height|visibility|x|y|d|transform|cx|cy|r)$/.test(E) && this.updateShadows(E, H, r)); this.doTransform && (this.updateTransform(), this.doTransform = !1) } e && e(); return B
            }, updateShadows: function (a,
                H, g) { for (var E = this.shadows, e = E.length; e--;)g.call(E[e], "height" === a ? Math.max(H - (E[e].cutHeight || 0), 0) : "d" === a ? this.d : H, a, E[e]) }, addClass: function (a, H) { var E = this.attr("class") || ""; -1 === E.indexOf(a) && (H || (a = (E + (E ? " " : "") + a).replace("  ", " ")), this.attr("class", a)); return this }, hasClass: function (a) { return -1 !== h(this.element, "class").indexOf(a) }, removeClass: function (a) { h(this.element, "class", (h(this.element, "class") || "").replace(a, "")); return this }, symbolAttr: function (a) {
                    var E = this; y("x y r start end width height innerR anchorX anchorY".split(" "),
                        function (g) { E[g] = K(a[g], E[g]) }); E.attr({ d: E.renderer.symbols[E.symbolName](E.x, E.y, E.width, E.height, E) })
                }, clip: function (a) { return this.attr("clip-path", a ? "url(" + this.renderer.url + "#" + a.id + ")" : "none") }, crisp: function (a, g) {
                    var E, H = {}, e; g = g || a.strokeWidth || 0; e = Math.round(g) % 2 / 2; a.x = Math.floor(a.x || this.x || 0) + e; a.y = Math.floor(a.y || this.y || 0) + e; a.width = Math.floor((a.width || this.width || 0) - 2 * e); a.height = Math.floor((a.height || this.height || 0) - 2 * e); u(a.strokeWidth) && (a.strokeWidth = g); for (E in a) this[E] !== a[E] &&
                        (this[E] = H[E] = a[E]); return H
                }, css: function (a) {
                    var g = this.styles, E = {}, k = this.element, b, c, r = ""; b = !g; a && a.color && (a.fill = a.color); if (g) for (c in a) a[c] !== g[c] && (E[c] = a[c], b = !0); if (b) {
                        b = this.textWidth = a && a.width && "text" === k.nodeName.toLowerCase() && J(a.width) || this.textWidth; g && (a = t(g, E)); this.styles = a; b && !B && this.renderer.forExport && delete a.width; if (e && !B) v(this.element, a); else { g = function (a, g) { return "-" + g.toLowerCase() }; for (c in a) r += c.replace(/([A-Z])/g, g) + ":" + a[c] + ";"; h(k, "style", r) } this.added && (b &&
                            this.renderer.buildText(this), a && a.textOutline && this.applyTextOutline(a.textOutline))
                    } return this
                }, strokeWidth: function () { return this["stroke-width"] || 0 }, on: function (a, g) { var E = this, e = E.element; q && "click" === a ? (e.ontouchstart = function (a) { E.touchEventFired = Date.now(); a.preventDefault(); g.call(e, a) }, e.onclick = function (a) { (-1 === R.navigator.userAgent.indexOf("Android") || 1100 < Date.now() - (E.touchEventFired || 0)) && g.call(e, a) }) : e["on" + a] = g; return this }, setRadialReference: function (a) {
                    var g = this.renderer.gradients[this.element.gradient];
                    this.element.radialReference = a; g && g.radAttr && g.animate(this.renderer.getRadialAttr(a, g.radAttr)); return this
                }, translate: function (a, g) { return this.attr({ translateX: a, translateY: g }) }, invert: function (a) { this.inverted = a; this.updateTransform(); return this }, updateTransform: function () {
                    var a = this.translateX || 0, g = this.translateY || 0, e = this.scaleX, k = this.scaleY, b = this.inverted, c = this.rotation, B = this.element; b && (a += this.attr("width"), g += this.attr("height")); a = ["translate(" + a + "," + g + ")"]; b ? a.push("rotate(90) scale(-1,1)") :
                        c && a.push("rotate(" + c + " " + (B.getAttribute("x") || 0) + " " + (B.getAttribute("y") || 0) + ")"); (u(e) || u(k)) && a.push("scale(" + K(e, 1) + " " + K(k, 1) + ")"); a.length && B.setAttribute("transform", a.join(" "))
                }, toFront: function () { var a = this.element; a.parentNode.appendChild(a); return this }, align: function (a, g, e) {
                    var E, H, k, b, c = {}; H = this.renderer; k = H.alignedObjects; var B, r; if (a) { if (this.alignOptions = a, this.alignByTranslate = g, !e || x(e)) this.alignTo = E = e || "renderer", m(k, this), k.push(this), e = null } else a = this.alignOptions, g = this.alignByTranslate,
                        E = this.alignTo; e = K(e, H[E], H); E = a.align; H = a.verticalAlign; k = (e.x || 0) + (a.x || 0); b = (e.y || 0) + (a.y || 0); "right" === E ? B = 1 : "center" === E && (B = 2); B && (k += (e.width - (a.width || 0)) / B); c[g ? "translateX" : "x"] = Math.round(k); "bottom" === H ? r = 1 : "middle" === H && (r = 2); r && (b += (e.height - (a.height || 0)) / r); c[g ? "translateY" : "y"] = Math.round(b); this[this.placed ? "animate" : "attr"](c); this.placed = !0; this.alignAttr = c; return this
                }, getBBox: function (a, g) {
                    var E, H = this.renderer, k, b = this.element, c = this.styles, B, r = this.textStr, m, w = H.cache, q = H.cacheKeys,
                    x; g = K(g, this.rotation); k = g * d; B = c && c.fontSize; void 0 !== r && (x = r.toString(), -1 === x.indexOf("\x3c") && (x = x.replace(/[0-9]/g, "0")), x += ["", g || 0, B, b.style.width, b.style["text-overflow"]].join()); x && !a && (E = w[x]); if (!E) {
                        if (b.namespaceURI === this.SVG_NS || H.forExport) {
                            try { (m = this.fakeTS && function (a) { y(b.querySelectorAll(".highcharts-text-outline"), function (g) { g.style.display = a }) }) && m("none"), E = b.getBBox ? t({}, b.getBBox()) : { width: b.offsetWidth, height: b.offsetHeight }, m && m("") } catch (T) { } if (!E || 0 > E.width) E = {
                                width: 0,
                                height: 0
                            }
                        } else E = this.htmlGetBBox(); H.isSVG && (a = E.width, H = E.height, e && c && "11px" === c.fontSize && "16.9" === H.toPrecision(3) && (E.height = H = 14), g && (E.width = Math.abs(H * Math.sin(k)) + Math.abs(a * Math.cos(k)), E.height = Math.abs(H * Math.cos(k)) + Math.abs(a * Math.sin(k)))); if (x && 0 < E.height) { for (; 250 < q.length;)delete w[q.shift()]; w[x] || q.push(x); w[x] = E }
                    } return E
                }, show: function (a) { return this.attr({ visibility: a ? "inherit" : "visible" }) }, hide: function () { return this.attr({ visibility: "hidden" }) }, fadeOut: function (a) {
                    var g =
                        this; g.animate({ opacity: 0 }, { duration: a || 150, complete: function () { g.attr({ y: -9999 }) } })
                }, add: function (a) { var g = this.renderer, e = this.element, E; a && (this.parentGroup = a); this.parentInverted = a && a.inverted; void 0 !== this.textStr && g.buildText(this); this.added = !0; if (!a || a.handleZ || this.zIndex) E = this.zIndexSetter(); E || (a ? a.element : g.box).appendChild(e); if (this.onAdd) this.onAdd(); return this }, safeRemoveChild: function (a) { var g = a.parentNode; g && g.removeChild(a) }, destroy: function () {
                    var a = this.element || {}, e = this.renderer.isSVG &&
                        "SPAN" === a.nodeName && this.parentGroup, k, b; a.onclick = a.onmouseout = a.onmouseover = a.onmousemove = a.point = null; g(this); this.clipPath && (this.clipPath = this.clipPath.destroy()); if (this.stops) { for (b = 0; b < this.stops.length; b++)this.stops[b] = this.stops[b].destroy(); this.stops = null } this.safeRemoveChild(a); for (this.destroyShadows(); e && e.div && 0 === e.div.childNodes.length;)a = e.parentGroup, this.safeRemoveChild(e.div), delete e.div, e = a; this.alignTo && m(this.renderer.alignedObjects, this); for (k in this) delete this[k]; return null
                },
            shadow: function (a, g, e) {
                var E = [], b, k, H = this.element, c, B, r, m; if (!a) this.destroyShadows(); else if (!this.shadows) {
                    B = K(a.width, 3); r = (a.opacity || .15) / B; m = this.parentInverted ? "(-1,-1)" : "(" + K(a.offsetX, 1) + ", " + K(a.offsetY, 1) + ")"; for (b = 1; b <= B; b++)k = H.cloneNode(0), c = 2 * B + 1 - 2 * b, h(k, { isShadow: "true", stroke: a.color || "#000000", "stroke-opacity": r * b, "stroke-width": c, transform: "translate" + m, fill: "none" }), e && (h(k, "height", Math.max(h(k, "height") - c, 0)), k.cutHeight = c), g ? g.element.appendChild(k) : H.parentNode.insertBefore(k,
                        H), E.push(k); this.shadows = E
                } return this
            }, destroyShadows: function () { y(this.shadows || [], function (a) { this.safeRemoveChild(a) }, this); this.shadows = void 0 }, xGetter: function (a) { "circle" === this.element.nodeName && ("x" === a ? a = "cx" : "y" === a && (a = "cy")); return this._defaultGetter(a) }, _defaultGetter: function (a) { a = K(this[a], this.element ? this.element.getAttribute(a) : null, 0); /^[\-0-9\.]+$/.test(a) && (a = parseFloat(a)); return a }, dSetter: function (a, g, e) {
            a && a.join && (a = a.join(" ")); /(NaN| {2}|^$)/.test(a) && (a = "M 0 0"); e.setAttribute(g,
                a); this[g] = a
            }, dashstyleSetter: function (a) { var g, e = this["stroke-width"]; "inherit" === e && (e = 1); if (a = a && a.toLowerCase()) { a = a.replace("shortdashdotdot", "3,1,1,1,1,1,").replace("shortdashdot", "3,1,1,1").replace("shortdot", "1,1,").replace("shortdash", "3,1,").replace("longdash", "8,3,").replace(/dot/g, "1,3,").replace("dash", "4,3,").replace(/,$/, "").split(","); for (g = a.length; g--;)a[g] = J(a[g]) * e; a = a.join(",").replace(/NaN/g, "none"); this.element.setAttribute("stroke-dasharray", a) } }, alignSetter: function (a) {
                this.element.setAttribute("text-anchor",
                    { left: "start", center: "middle", right: "end" }[a])
            }, opacitySetter: function (a, g, e) { this[g] = a; e.setAttribute(g, a) }, titleSetter: function (a) { var g = this.element.getElementsByTagName("title")[0]; g || (g = n.createElementNS(this.SVG_NS, "title"), this.element.appendChild(g)); g.firstChild && g.removeChild(g.firstChild); g.appendChild(n.createTextNode(String(K(a), "").replace(/<[^>]*>/g, ""))) }, textSetter: function (a) { a !== this.textStr && (delete this.bBox, this.textStr = a, this.added && this.renderer.buildText(this)) }, fillSetter: function (a,
                g, e) { "string" === typeof a ? e.setAttribute(g, a) : a && this.colorGradient(a, g, e) }, visibilitySetter: function (a, g, e) { "inherit" === a ? e.removeAttribute(g) : e.setAttribute(g, a) }, zIndexSetter: function (a, g) {
                    var e = this.renderer, k = this.parentGroup, b = (k || e).element || e.box, c, B = this.element, H; c = this.added; var r; u(a) && (B.zIndex = a, a = +a, this[g] === a && (c = !1), this[g] = a); if (c) {
                    (a = this.zIndex) && k && (k.handleZ = !0); g = b.childNodes; for (r = 0; r < g.length && !H; r++)k = g[r], c = k.zIndex, k !== B && (J(c) > a || !u(a) && u(c) || 0 > a && !u(c) && b !== e.box) && (b.insertBefore(B,
                        k), H = !0); H || b.appendChild(B)
                    } return H
                }, _defaultSetter: function (a, g, e) { e.setAttribute(g, a) }
        }; D.prototype.yGetter = D.prototype.xGetter; D.prototype.translateXSetter = D.prototype.translateYSetter = D.prototype.rotationSetter = D.prototype.verticalAlignSetter = D.prototype.scaleXSetter = D.prototype.scaleYSetter = function (a, g) { this[g] = a; this.doTransform = !0 }; D.prototype["stroke-widthSetter"] = D.prototype.strokeSetter = function (a, g, e) {
        this[g] = a; this.stroke && this["stroke-width"] ? (D.prototype.fillSetter.call(this, this.stroke,
            "stroke", e), e.setAttribute("stroke-width", this["stroke-width"]), this.hasStroke = !0) : "stroke-width" === g && 0 === a && this.hasStroke && (e.removeAttribute("stroke"), this.hasStroke = !1)
        }; C = a.SVGRenderer = function () { this.init.apply(this, arguments) }; C.prototype = {
            Element: D, SVG_NS: S, init: function (a, g, e, k, b, c) {
                var B; k = this.createElement("svg").attr({ version: "1.1", "class": "highcharts-root" }).css(this.getStyle(k)); B = k.element; a.appendChild(B); -1 === a.innerHTML.indexOf("xmlns") && h(B, "xmlns", this.SVG_NS); this.isSVG = !0;
                this.box = B; this.boxWrapper = k; this.alignedObjects = []; this.url = (F || A) && n.getElementsByTagName("base").length ? R.location.href.replace(/#.*?$/, "").replace(/([\('\)])/g, "\\$1").replace(/ /g, "%20") : ""; this.createElement("desc").add().element.appendChild(n.createTextNode("Created with Highcharts 5.0.6")); this.defs = this.createElement("defs").add(); this.allowHTML = c; this.forExport = b; this.gradients = {}; this.cache = {}; this.cacheKeys = []; this.imgCount = 0; this.setSize(g, e, !1); var H; F && a.getBoundingClientRect && (g = function () {
                    v(a,
                        { left: 0, top: 0 }); H = a.getBoundingClientRect(); v(a, { left: Math.ceil(H.left) - H.left + "px", top: Math.ceil(H.top) - H.top + "px" })
                }, g(), this.unSubPixelFix = G(R, "resize", g))
            }, getStyle: function (a) { return this.style = t({ fontFamily: '"Iransans","B Nazanin","Lucida Grande", "Lucida Sans Unicode", Arial, Helvetica, sans-serif', fontSize: "12px" }, a) }, setStyle: function (a) { this.boxWrapper.css(this.getStyle(a)) }, isHidden: function () { return !this.boxWrapper.getBBox().width }, destroy: function () {
                var a = this.defs; this.box = null; this.boxWrapper = this.boxWrapper.destroy();
                c(this.gradients || {}); this.gradients = null; a && (this.defs = a.destroy()); this.unSubPixelFix && this.unSubPixelFix(); return this.alignedObjects = null
            }, createElement: function (a) { var g = new this.Element; g.init(this, a); return g }, draw: w, getRadialAttr: function (a, g) { return { cx: a[0] - a[2] / 2 + g.cx * a[2], cy: a[1] - a[2] / 2 + g.cy * a[2], r: g.r * a[2] } }, buildText: function (a) {
                for (var g = a.element, e = this, k = e.forExport, c = K(a.textStr, "").toString(), r = -1 !== c.indexOf("\x3c"), m = g.childNodes, w, E, q, x, d = h(g, "x"), t = a.styles, A = a.textWidth, z = t &&
                    t.lineHeight, l = t && t.textOutline, F = t && "ellipsis" === t.textOverflow, f = m.length, u = A && !a.added && this.box, p = function (a) { var k; k = /(px|em)$/.test(a && a.style.fontSize) ? a.style.fontSize : t && t.fontSize || e.style.fontSize || 12; return z ? J(z) : e.fontMetrics(k, a.getAttribute("style") ? a : g).h }; f--;)g.removeChild(m[f]); r || l || F || A || -1 !== c.indexOf(" ") ? (w = /<.*class="([^"]+)".*>/, E = /<.*style="([^"]+)".*>/, q = /<.*href="(http[^"]+)".*>/, u && u.appendChild(g), c = r ? c.replace(/<(b|strong)>/g, '\x3cspan style\x3d"font-weight:bold"\x3e').replace(/<(i|em)>/g,
                        '\x3cspan style\x3d"font-style:italic"\x3e').replace(/<a/g, "\x3cspan").replace(/<\/(b|strong|i|em|a)>/g, "\x3c/span\x3e").split(/<br.*?>/g) : [c], c = b(c, function (a) { return "" !== a }), y(c, function (b, c) {
                            var r, H = 0; b = b.replace(/^\s+|\s+$/g, "").replace(/<span/g, "|||\x3cspan").replace(/<\/span>/g, "\x3c/span\x3e|||"); r = b.split("|||"); y(r, function (b) {
                                if ("" !== b || 1 === r.length) {
                                    var m = {}, l = n.createElementNS(e.SVG_NS, "tspan"), z, f; w.test(b) && (z = b.match(w)[1], h(l, "class", z)); E.test(b) && (f = b.match(E)[1].replace(/(;| |^)color([ :])/,
                                        "$1fill$2"), h(l, "style", f)); q.test(b) && !k && (h(l, "onclick", 'location.href\x3d"' + b.match(q)[1] + '"'), v(l, { cursor: "pointer" })); b = (b.replace(/<(.|\n)*?>/g, "") || " ").replace(/&lt;/g, "\x3c").replace(/&gt;/g, "\x3e"); if (" " !== b) {
                                            l.appendChild(n.createTextNode(b)); H ? m.dx = 0 : c && null !== d && (m.x = d); h(l, m); g.appendChild(l); !H && c && (!B && k && v(l, { display: "block" }), h(l, "dy", p(l))); if (A) {
                                                m = b.replace(/([^\^])-/g, "$1- ").split(" "); z = "nowrap" === t.whiteSpace; for (var K = 1 < r.length || c || 1 < m.length && !z, u, y, J = [], M = p(l), P = a.rotation,
                                                    O = b, N = O.length; (K || F) && (m.length || J.length);)a.rotation = 0, u = a.getBBox(!0), y = u.width, !B && e.forExport && (y = e.measureSpanWidth(l.firstChild.data, a.styles)), u = y > A, void 0 === x && (x = u), F && x ? (N /= 2, "" === O || !u && .5 > N ? m = [] : (O = b.substring(0, O.length + (u ? -1 : 1) * Math.ceil(N)), m = [O + (3 < A ? "\u2026" : "")], l.removeChild(l.firstChild))) : u && 1 !== m.length ? (l.removeChild(l.firstChild), J.unshift(m.pop())) : (m = J, J = [], m.length && !z && (l = n.createElementNS(S, "tspan"), h(l, { dy: M, x: d }), f && h(l, "style", f), g.appendChild(l)), y > A && (A = y)), m.length &&
                                                        l.appendChild(n.createTextNode(m.join(" ").replace(/- /g, "-"))); a.rotation = P
                                            } H++
                                        }
                                }
                            })
                        }), x && a.attr("title", a.textStr), u && u.removeChild(g), l && a.applyTextOutline && a.applyTextOutline(l)) : g.appendChild(n.createTextNode(c.replace(/&lt;/g, "\x3c").replace(/&gt;/g, "\x3e")))
            }, getContrast: function (a) { a = p(a).rgba; return 510 < a[0] + a[1] + a[2] ? "#000000" : "#FFFFFF" }, button: function (a, g, b, c, B, r, m, w, q) {
                var H = this.label(a, g, b, q, null, null, null, null, "button"), E = 0; H.attr(k({ padding: 8, r: 2 }, B)); var x, d, n, l; B = k({
                    fill: "#f7f7f7",
                    stroke: "#cccccc", "stroke-width": 1, style: { color: "#333333", cursor: "pointer", fontWeight: "normal" }
                }, B); x = B.style; delete B.style; r = k(B, { fill: "#e6e6e6" }, r); d = r.style; delete r.style; m = k(B, { fill: "#e6ebf5", style: { color: "#000000", fontWeight: "bold" } }, m); n = m.style; delete m.style; w = k(B, { style: { color: "#cccccc" } }, w); l = w.style; delete w.style; G(H.element, e ? "mouseover" : "mouseenter", function () { 3 !== E && H.setState(1) }); G(H.element, e ? "mouseout" : "mouseleave", function () { 3 !== E && H.setState(E) }); H.setState = function (a) {
                1 !== a &&
                    (H.state = E = a); H.removeClass(/highcharts-button-(normal|hover|pressed|disabled)/).addClass("highcharts-button-" + ["normal", "hover", "pressed", "disabled"][a || 0]); H.attr([B, r, m, w][a || 0]).css([x, d, n, l][a || 0])
                }; H.attr(B).css(t({ cursor: "default" }, x)); return H.on("click", function (a) { 3 !== E && c.call(H, a) })
            }, crispLine: function (a, g) { a[1] === a[4] && (a[1] = a[4] = Math.round(a[1]) - g % 2 / 2); a[2] === a[5] && (a[2] = a[5] = Math.round(a[2]) + g % 2 / 2); return a }, path: function (a) { var g = { fill: "none" }; z(a) ? g.d = a : r(a) && t(g, a); return this.createElement("path").attr(g) },
            circle: function (a, g, e) { a = r(a) ? a : { x: a, y: g, r: e }; g = this.createElement("circle"); g.xSetter = g.ySetter = function (a, g, e) { e.setAttribute("c" + g, a) }; return g.attr(a) }, arc: function (a, g, e, b, k, c) { r(a) && (g = a.y, e = a.r, b = a.innerR, k = a.start, c = a.end, a = a.x); a = this.symbol("arc", a || 0, g || 0, e || 0, e || 0, { innerR: b || 0, start: k || 0, end: c || 0 }); a.r = e; return a }, rect: function (a, g, e, b, k, c) {
                k = r(a) ? a.r : k; var B = this.createElement("rect"); a = r(a) ? a : void 0 === a ? {} : { x: a, y: g, width: Math.max(e, 0), height: Math.max(b, 0) }; void 0 !== c && (a.strokeWidth =
                    c, a = B.crisp(a)); a.fill = "none"; k && (a.r = k); B.rSetter = function (a, g, e) { h(e, { rx: a, ry: a }) }; return B.attr(a)
            }, setSize: function (a, g, e) { var b = this.alignedObjects, k = b.length; this.width = a; this.height = g; for (this.boxWrapper.animate({ width: a, height: g }, { step: function () { this.attr({ viewBox: "0 0 " + this.attr("width") + " " + this.attr("height") }) }, duration: K(e, !0) ? void 0 : 0 }); k--;)b[k].align() }, g: function (a) { var g = this.createElement("g"); return a ? g.attr({ "class": "highcharts-" + a }) : g }, image: function (a, g, e, b, k) {
                var c = { preserveAspectRatio: "none" };
                1 < arguments.length && t(c, { x: g, y: e, width: b, height: k }); c = this.createElement("image").attr(c); c.element.setAttributeNS ? c.element.setAttributeNS("http://www.w3.org/1999/xlink", "href", a) : c.element.setAttribute("hc-svg-href", a); return c
            }, symbol: function (a, g, e, b, k, c) {
                var B = this, r, H = this.symbols[a], m = u(g) && H && H(Math.round(g), Math.round(e), b, k, c), w = /^url\((.*?)\)$/, q, x; H ? (r = this.path(m), r.attr("fill", "none"), t(r, { symbolName: a, x: g, y: e, width: b, height: k }), c && t(r, c)) : w.test(a) && (q = a.match(w)[1], r = this.image(q),
                    r.imgwidth = K(M[q] && M[q].width, c && c.width), r.imgheight = K(M[q] && M[q].height, c && c.height), x = function () { r.attr({ width: r.width, height: r.height }) }, y(["width", "height"], function (a) { r[a + "Setter"] = function (a, g) { var e = {}, b = this["img" + g], k = "width" === g ? "translateX" : "translateY"; this[g] = a; u(b) && (this.element && this.element.setAttribute(g, b), this.alignByTranslate || (e[k] = ((this[g] || 0) - b) / 2, this.attr(e))) } }), u(g) && r.attr({ x: g, y: e }), r.isImg = !0, u(r.imgwidth) && u(r.imgheight) ? x() : (r.attr({ width: 0, height: 0 }), l("img", {
                        onload: function () {
                            var a =
                                f[B.chartIndex]; 0 === this.width && (v(this, { position: "absolute", top: "-999em" }), n.body.appendChild(this)); M[q] = { width: this.width, height: this.height }; r.imgwidth = this.width; r.imgheight = this.height; r.element && x(); this.parentNode && this.parentNode.removeChild(this); B.imgCount--; if (!B.imgCount && a && a.onload) a.onload()
                        }, src: q
                    }), this.imgCount++)); return r
            }, symbols: {
                circle: function (a, g, e, b) { var k = .166 * e; return ["M", a + e / 2, g, "C", a + e + k, g, a + e + k, g + b, a + e / 2, g + b, "C", a - k, g + b, a - k, g, a + e / 2, g, "Z"] }, square: function (a, g, e, b) {
                    return ["M",
                        a, g, "L", a + e, g, a + e, g + b, a, g + b, "Z"]
                }, triangle: function (a, g, e, b) { return ["M", a + e / 2, g, "L", a + e, g + b, a, g + b, "Z"] }, "triangle-down": function (a, g, e, b) { return ["M", a, g, "L", a + e, g, a + e / 2, g + b, "Z"] }, diamond: function (a, g, e, b) { return ["M", a + e / 2, g, "L", a + e, g + b / 2, a + e / 2, g + b, a, g + b / 2, "Z"] }, arc: function (a, g, e, b, k) {
                    var c = k.start; e = k.r || e || b; var B = k.end - .001; b = k.innerR; var r = k.open, m = Math.cos(c), H = Math.sin(c), w = Math.cos(B), B = Math.sin(B); k = k.end - c < Math.PI ? 0 : 1; return ["M", a + e * m, g + e * H, "A", e, e, 0, k, 1, a + e * w, g + e * B, r ? "M" : "L", a + b * w, g + b * B,
                        "A", b, b, 0, k, 0, a + b * m, g + b * H, r ? "" : "Z"]
                }, callout: function (a, g, e, b, k) {
                    var c = Math.min(k && k.r || 0, e, b), B = c + 6, r = k && k.anchorX; k = k && k.anchorY; var m; m = ["M", a + c, g, "L", a + e - c, g, "C", a + e, g, a + e, g, a + e, g + c, "L", a + e, g + b - c, "C", a + e, g + b, a + e, g + b, a + e - c, g + b, "L", a + c, g + b, "C", a, g + b, a, g + b, a, g + b - c, "L", a, g + c, "C", a, g, a, g, a + c, g]; r && r > e ? k > g + B && k < g + b - B ? m.splice(13, 3, "L", a + e, k - 6, a + e + 6, k, a + e, k + 6, a + e, g + b - c) : m.splice(13, 3, "L", a + e, b / 2, r, k, a + e, b / 2, a + e, g + b - c) : r && 0 > r ? k > g + B && k < g + b - B ? m.splice(33, 3, "L", a, k + 6, a - 6, k, a, k - 6, a, g + c) : m.splice(33, 3, "L",
                        a, b / 2, r, k, a, b / 2, a, g + c) : k && k > b && r > a + B && r < a + e - B ? m.splice(23, 3, "L", r + 6, g + b, r, g + b + 6, r - 6, g + b, a + c, g + b) : k && 0 > k && r > a + B && r < a + e - B && m.splice(3, 3, "L", r - 6, g, r, g - 6, r + 6, g, e - c, g); return m
                }
            }, clipRect: function (g, e, b, k) { var c = a.uniqueKey(), B = this.createElement("clipPath").attr({ id: c }).add(this.defs); g = this.rect(g, e, b, k, 0).add(B); g.id = c; g.clipPath = B; g.count = 0; return g }, text: function (a, g, e, b) {
                var k = !B && this.forExport, c = {}; if (b && (this.allowHTML || !this.forExport)) return this.html(a, g, e); c.x = Math.round(g || 0); e && (c.y = Math.round(e));
                if (a || 0 === a) c.text = a; a = this.createElement("text").attr(c); k && a.css({ position: "absolute" }); b || (a.xSetter = function (a, g, e) { var b = e.getElementsByTagName("tspan"), k, c = e.getAttribute(g), B; for (B = 0; B < b.length; B++)k = b[B], k.getAttribute(g) === c && k.setAttribute(g, a); e.setAttribute(g, a) }); return a
            }, fontMetrics: function (a, g) {
                a = a || g && g.style && g.style.fontSize || this.style && this.style.fontSize; a = /px/.test(a) ? J(a) : /em/.test(a) ? parseFloat(a) * (g ? this.fontMetrics(null, g.parentNode).f : 16) : 12; g = 24 > a ? a + 3 : Math.round(1.2 *
                    a); return { h: g, b: Math.round(.8 * g), f: a }
            }, rotCorr: function (a, g, e) { var b = a; g && e && (b = Math.max(b * Math.cos(g * d), 4)); return { x: -a / 3 * Math.sin(g * d), y: b } }, label: function (a, g, e, b, c, B, r, m, w) {
                var q = this, x = q.g("button" !== w && "label"), d = x.text = q.text("", 0, 0, r).attr({ zIndex: 1 }), H, n, l = 0, A = 3, z = 0, F, f, K, p, J, h = {}, M, S, E = /^url\((.*?)\)$/.test(b), v = E, P, R, O, Q; w && x.addClass("highcharts-" + w); v = E; P = function () { return (M || 0) % 2 / 2 }; R = function () {
                    var a = d.element.style, g = {}; n = (void 0 === F || void 0 === f || J) && u(d.textStr) && d.getBBox(); x.width =
                        (F || n.width || 0) + 2 * A + z; x.height = (f || n.height || 0) + 2 * A; S = A + q.fontMetrics(a && a.fontSize, d).b; v && (H || (x.box = H = q.symbols[b] || E ? q.symbol(b) : q.rect(), H.addClass(("button" === w ? "" : "highcharts-label-box") + (w ? " highcharts-" + w + "-box" : "")), H.add(x), a = P(), g.x = a, g.y = (m ? -S : 0) + a), g.width = Math.round(x.width), g.height = Math.round(x.height), H.attr(t(g, h)), h = {})
                }; O = function () {
                    var a = z + A, g; g = m ? 0 : S; u(F) && n && ("center" === J || "right" === J) && (a += { center: .5, right: 1 }[J] * (F - n.width)); if (a !== d.x || g !== d.y) d.attr("x", a), void 0 !== g && d.attr("y",
                        g); d.x = a; d.y = g
                }; Q = function (a, g) { H ? H.attr(a, g) : h[a] = g }; x.onAdd = function () { d.add(x); x.attr({ text: a || 0 === a ? a : "", x: g, y: e }); H && u(c) && x.attr({ anchorX: c, anchorY: B }) }; x.widthSetter = function (a) { F = a }; x.heightSetter = function (a) { f = a }; x["text-alignSetter"] = function (a) { J = a }; x.paddingSetter = function (a) { u(a) && a !== A && (A = x.padding = a, O()) }; x.paddingLeftSetter = function (a) { u(a) && a !== z && (z = a, O()) }; x.alignSetter = function (a) { a = { left: 0, center: .5, right: 1 }[a]; a !== l && (l = a, n && x.attr({ x: K })) }; x.textSetter = function (a) {
                void 0 !==
                    a && d.textSetter(a); R(); O()
                }; x["stroke-widthSetter"] = function (a, g) { a && (v = !0); M = this["stroke-width"] = a; Q(g, a) }; x.strokeSetter = x.fillSetter = x.rSetter = function (a, g) { "fill" === g && a && (v = !0); Q(g, a) }; x.anchorXSetter = function (a, g) { c = a; Q(g, Math.round(a) - P() - K) }; x.anchorYSetter = function (a, g) { B = a; Q(g, a - p) }; x.xSetter = function (a) { x.x = a; l && (a -= l * ((F || n.width) + 2 * A)); K = Math.round(a); x.attr("translateX", K) }; x.ySetter = function (a) { p = x.y = Math.round(a); x.attr("translateY", p) }; var V = x.css; return t(x, {
                    css: function (a) {
                        if (a) {
                            var g =
                                {}; a = k(a); y(x.textProps, function (e) { void 0 !== a[e] && (g[e] = a[e], delete a[e]) }); d.css(g)
                        } return V.call(x, a)
                    }, getBBox: function () { return { width: n.width + 2 * A, height: n.height + 2 * A, x: n.x - A, y: n.y - A } }, shadow: function (a) { a && (R(), H && H.shadow(a)); return x }, destroy: function () { N(x.element, "mouseenter"); N(x.element, "mouseleave"); d && (d = d.destroy()); H && (H = H.destroy()); D.prototype.destroy.call(x); x = q = R = O = Q = null }
                })
            }
        }; a.Renderer = C
    })(L); (function (a) {
        var D = a.attr, C = a.createElement, G = a.css, I = a.defined, h = a.each, f = a.extend, p =
            a.isFirefox, v = a.isMS, l = a.isWebKit, u = a.pInt, d = a.SVGRenderer, c = a.win, n = a.wrap; f(a.SVGElement.prototype, {
                htmlCss: function (a) { var c = this.element; if (c = a && "SPAN" === c.tagName && a.width) delete a.width, this.textWidth = c, this.updateTransform(); a && "ellipsis" === a.textOverflow && (a.whiteSpace = "nowrap", a.overflow = "hidden"); this.styles = f(this.styles, a); G(this.element, a); return this }, htmlGetBBox: function () {
                    var a = this.element; "text" === a.nodeName && (a.style.position = "absolute"); return {
                        x: a.offsetLeft, y: a.offsetTop, width: a.offsetWidth,
                        height: a.offsetHeight
                    }
                }, htmlUpdateTransform: function () {
                    if (this.added) {
                        var a = this.renderer, c = this.element, m = this.translateX || 0, b = this.translateY || 0, q = this.x || 0, d = this.y || 0, n = this.textAlign || "left", e = { left: 0, center: .5, right: 1 }[n], r = this.styles; G(c, { marginLeft: m, marginTop: b }); this.shadows && h(this.shadows, function (a) { G(a, { marginLeft: m + 1, marginTop: b + 1 }) }); this.inverted && h(c.childNodes, function (e) { a.invertChild(e, c) }); if ("SPAN" === c.tagName) {
                            var x = this.rotation, A = u(this.textWidth), k = r && r.whiteSpace, w = [x,
                                n, c.innerHTML, this.textWidth, this.textAlign].join(); w !== this.cTT && (r = a.fontMetrics(c.style.fontSize).b, I(x) && this.setSpanRotation(x, e, r), G(c, { width: "", whiteSpace: k || "nowrap" }), c.offsetWidth > A && /[ \-]/.test(c.textContent || c.innerText) && G(c, { width: A + "px", display: "block", whiteSpace: k || "normal" }), this.getSpanCorrection(c.offsetWidth, r, e, x, n)); G(c, { left: q + (this.xCorr || 0) + "px", top: d + (this.yCorr || 0) + "px" }); l && (r = c.offsetHeight); this.cTT = w
                        }
                    } else this.alignOnAdd = !0
                }, setSpanRotation: function (a, d, m) {
                    var b = {},
                    q = v ? "-ms-transform" : l ? "-webkit-transform" : p ? "MozTransform" : c.opera ? "-o-transform" : ""; b[q] = b.transform = "rotate(" + a + "deg)"; b[q + (p ? "Origin" : "-origin")] = b.transformOrigin = 100 * d + "% " + m + "px"; G(this.element, b)
                }, getSpanCorrection: function (a, c, m) { this.xCorr = -a * m; this.yCorr = -c }
            }); f(d.prototype, {
                html: function (a, c, m) {
                    var b = this.createElement("span"), q = b.element, d = b.renderer, l = d.isSVG, e = function (a, e) { h(["opacity", "visibility"], function (b) { n(a, b + "Setter", function (a, b, c, r) { a.call(this, b, c, r); e[c] = b }) }) }; b.textSetter =
                        function (a) { a !== q.innerHTML && delete this.bBox; q.innerHTML = this.textStr = a; b.htmlUpdateTransform() }; l && e(b, b.element.style); b.xSetter = b.ySetter = b.alignSetter = b.rotationSetter = function (a, e) { "align" === e && (e = "textAlign"); b[e] = a; b.htmlUpdateTransform() }; b.attr({ text: a, x: Math.round(c), y: Math.round(m) }).css({ fontFamily: this.style.fontFamily, fontSize: this.style.fontSize, position: "absolute" }); q.style.whiteSpace = "nowrap"; b.css = b.htmlCss; l && (b.add = function (a) {
                            var c, r = d.box.parentNode, k = []; if (this.parentGroup =
                                a) {
                                    if (c = a.div, !c) {
                                        for (; a;)k.push(a), a = a.parentGroup; h(k.reverse(), function (a) {
                                            var m, x = D(a.element, "class"); x && (x = { className: x }); c = a.div = a.div || C("div", x, { position: "absolute", left: (a.translateX || 0) + "px", top: (a.translateY || 0) + "px", display: a.display, opacity: a.opacity, pointerEvents: a.styles && a.styles.pointerEvents }, c || r); m = c.style; f(a, {
                                                on: function () { b.on.apply({ element: k[0].div }, arguments); return a }, translateXSetter: function (e, g) { m.left = e + "px"; a[g] = e; a.doTransform = !0 }, translateYSetter: function (e, g) {
                                                m.top =
                                                    e + "px"; a[g] = e; a.doTransform = !0
                                                }
                                            }); e(a, m)
                                        })
                                    }
                            } else c = r; c.appendChild(q); b.added = !0; b.alignOnAdd && b.htmlUpdateTransform(); return b
                        }); return b
                }
            })
    })(L); (function (a) {
        var D, C, G = a.createElement, I = a.css, h = a.defined, f = a.deg2rad, p = a.discardElement, v = a.doc, l = a.each, u = a.erase, d = a.extend; D = a.extendClass; var c = a.isArray, n = a.isNumber, y = a.isObject, t = a.merge; C = a.noop; var m = a.pick, b = a.pInt, q = a.SVGElement, z = a.SVGRenderer, F = a.win; a.svg || (C = {
            docMode8: v && 8 === v.documentMode, init: function (a, b) {
                var e = ["\x3c", b, ' filled\x3d"f" stroked\x3d"f"'],
                c = ["position: ", "absolute", ";"], k = "div" === b; ("shape" === b || k) && c.push("left:0;top:0;width:1px;height:1px;"); c.push("visibility: ", k ? "hidden" : "visible"); e.push(' style\x3d"', c.join(""), '"/\x3e'); b && (e = k || "span" === b || "img" === b ? e.join("") : a.prepVML(e), this.element = G(e)); this.renderer = a
            }, add: function (a) {
                var e = this.renderer, b = this.element, c = e.box, k = a && a.inverted, c = a ? a.element || a : c; a && (this.parentGroup = a); k && e.invertChild(b, c); c.appendChild(b); this.added = !0; this.alignOnAdd && !this.deferUpdateTransform && this.updateTransform();
                if (this.onAdd) this.onAdd(); this.className && this.attr("class", this.className); return this
            }, updateTransform: q.prototype.htmlUpdateTransform, setSpanRotation: function () { var a = this.rotation, b = Math.cos(a * f), c = Math.sin(a * f); I(this.element, { filter: a ? ["progid:DXImageTransform.Microsoft.Matrix(M11\x3d", b, ", M12\x3d", -c, ", M21\x3d", c, ", M22\x3d", b, ", sizingMethod\x3d'auto expand')"].join("") : "none" }) }, getSpanCorrection: function (a, b, c, q, k) {
                var e = q ? Math.cos(q * f) : 1, r = q ? Math.sin(q * f) : 0, x = m(this.elemHeight, this.element.offsetHeight),
                d; this.xCorr = 0 > e && -a; this.yCorr = 0 > r && -x; d = 0 > e * r; this.xCorr += r * b * (d ? 1 - c : c); this.yCorr -= e * b * (q ? d ? c : 1 - c : 1); k && "left" !== k && (this.xCorr -= a * c * (0 > e ? -1 : 1), q && (this.yCorr -= x * c * (0 > r ? -1 : 1)), I(this.element, { textAlign: k }))
            }, pathToVML: function (a) { for (var e = a.length, b = []; e--;)n(a[e]) ? b[e] = Math.round(10 * a[e]) - 5 : "Z" === a[e] ? b[e] = "x" : (b[e] = a[e], !a.isArc || "wa" !== a[e] && "at" !== a[e] || (b[e + 5] === b[e + 7] && (b[e + 7] += a[e + 7] > a[e + 5] ? 1 : -1), b[e + 6] === b[e + 8] && (b[e + 8] += a[e + 8] > a[e + 6] ? 1 : -1))); return b.join(" ") || "x" }, clip: function (a) {
                var e =
                    this, b; a ? (b = a.members, u(b, e), b.push(e), e.destroyClip = function () { u(b, e) }, a = a.getCSS(e)) : (e.destroyClip && e.destroyClip(), a = { clip: e.docMode8 ? "inherit" : "rect(auto)" }); return e.css(a)
            }, css: q.prototype.htmlCss, safeRemoveChild: function (a) { a.parentNode && p(a) }, destroy: function () { this.destroyClip && this.destroyClip(); return q.prototype.destroy.apply(this) }, on: function (a, b) { this.element["on" + a] = function () { var a = F.event; a.target = a.srcElement; b(a) }; return this }, cutOffPath: function (a, c) {
                var e; a = a.split(/[ ,]/);
                e = a.length; if (9 === e || 11 === e) a[e - 4] = a[e - 2] = b(a[e - 2]) - 10 * c; return a.join(" ")
            }, shadow: function (a, c, q) {
                var e = [], k, r = this.element, d = this.renderer, x, n = r.style, g, B = r.path, l, t, z, f; B && "string" !== typeof B.value && (B = "x"); t = B; if (a) {
                    z = m(a.width, 3); f = (a.opacity || .15) / z; for (k = 1; 3 >= k; k++)l = 2 * z + 1 - 2 * k, q && (t = this.cutOffPath(B.value, l + .5)), g = ['\x3cshape isShadow\x3d"true" strokeweight\x3d"', l, '" filled\x3d"false" path\x3d"', t, '" coordsize\x3d"10 10" style\x3d"', r.style.cssText, '" /\x3e'], x = G(d.prepVML(g), null, {
                        left: b(n.left) +
                            m(a.offsetX, 1), top: b(n.top) + m(a.offsetY, 1)
                    }), q && (x.cutOff = l + 1), g = ['\x3cstroke color\x3d"', a.color || "#000000", '" opacity\x3d"', f * k, '"/\x3e'], G(d.prepVML(g), null, null, x), c ? c.element.appendChild(x) : r.parentNode.insertBefore(x, r), e.push(x); this.shadows = e
                } return this
            }, updateShadows: C, setAttr: function (a, b) { this.docMode8 ? this.element[a] = b : this.element.setAttribute(a, b) }, classSetter: function (a) { (this.added ? this.element : this).className = a }, dashstyleSetter: function (a, b, c) {
            (c.getElementsByTagName("stroke")[0] ||
                G(this.renderer.prepVML(["\x3cstroke/\x3e"]), null, null, c))[b] = a || "solid"; this[b] = a
            }, dSetter: function (a, b, c) { var e = this.shadows; a = a || []; this.d = a.join && a.join(" "); c.path = a = this.pathToVML(a); if (e) for (c = e.length; c--;)e[c].path = e[c].cutOff ? this.cutOffPath(a, e[c].cutOff) : a; this.setAttr(b, a) }, fillSetter: function (a, b, c) { var e = c.nodeName; "SPAN" === e ? c.style.color = a : "IMG" !== e && (c.filled = "none" !== a, this.setAttr("fillcolor", this.renderer.color(a, c, b, this))) }, "fill-opacitySetter": function (a, b, c) {
                G(this.renderer.prepVML(["\x3c",
                    b.split("-")[0], ' opacity\x3d"', a, '"/\x3e']), null, null, c)
            }, opacitySetter: C, rotationSetter: function (a, b, c) { c = c.style; this[b] = c[b] = a; c.left = -Math.round(Math.sin(a * f) + 1) + "px"; c.top = Math.round(Math.cos(a * f)) + "px" }, strokeSetter: function (a, b, c) { this.setAttr("strokecolor", this.renderer.color(a, c, b, this)) }, "stroke-widthSetter": function (a, b, c) { c.stroked = !!a; this[b] = a; n(a) && (a += "px"); this.setAttr("strokeweight", a) }, titleSetter: function (a, b) { this.setAttr(b, a) }, visibilitySetter: function (a, b, c) {
            "inherit" === a &&
                (a = "visible"); this.shadows && l(this.shadows, function (c) { c.style[b] = a }); "DIV" === c.nodeName && (a = "hidden" === a ? "-999em" : 0, this.docMode8 || (c.style[b] = a ? "visible" : "hidden"), b = "top"); c.style[b] = a
            }, xSetter: function (a, b, c) { this[b] = a; "x" === b ? b = "left" : "y" === b && (b = "top"); this.updateClipping ? (this[b] = a, this.updateClipping()) : c.style[b] = a }, zIndexSetter: function (a, b, c) { c.style[b] = a }
        }, C["stroke-opacitySetter"] = C["fill-opacitySetter"], a.VMLElement = C = D(q, C), C.prototype.ySetter = C.prototype.widthSetter = C.prototype.heightSetter =
        C.prototype.xSetter, C = {
            Element: C, isIE8: -1 < F.navigator.userAgent.indexOf("MSIE 8.0"), init: function (a, b, c) {
                var e, k; this.alignedObjects = []; e = this.createElement("div").css({ position: "relative" }); k = e.element; a.appendChild(e.element); this.isVML = !0; this.box = k; this.boxWrapper = e; this.gradients = {}; this.cache = {}; this.cacheKeys = []; this.imgCount = 0; this.setSize(b, c, !1); if (!v.namespaces.hcv) {
                    v.namespaces.add("hcv", "urn:schemas-microsoft-com:vml"); try { v.createStyleSheet().cssText = "hcv\\:fill, hcv\\:path, hcv\\:shape, hcv\\:stroke{ behavior:url(#default#VML); display: inline-block; } " } catch (w) {
                    v.styleSheets[0].cssText +=
                        "hcv\\:fill, hcv\\:path, hcv\\:shape, hcv\\:stroke{ behavior:url(#default#VML); display: inline-block; } "
                    }
                }
            }, isHidden: function () { return !this.box.offsetWidth }, clipRect: function (a, b, c, m) {
                var e = this.createElement(), r = y(a); return d(e, {
                    members: [], count: 0, left: (r ? a.x : a) + 1, top: (r ? a.y : b) + 1, width: (r ? a.width : c) - 1, height: (r ? a.height : m) - 1, getCSS: function (a) {
                        var b = a.element, c = b.nodeName, g = a.inverted, e = this.top - ("shape" === c ? b.offsetTop : 0), k = this.left, b = k + this.width, m = e + this.height, e = {
                            clip: "rect(" + Math.round(g ?
                                k : e) + "px," + Math.round(g ? m : b) + "px," + Math.round(g ? b : m) + "px," + Math.round(g ? e : k) + "px)"
                        }; !g && a.docMode8 && "DIV" === c && d(e, { width: b + "px", height: m + "px" }); return e
                    }, updateClipping: function () { l(e.members, function (a) { a.element && a.css(e.getCSS(a)) }) }
                })
            }, color: function (b, c, m, q) {
                var e = this, r, d = /^rgba/, n, x, g = "none"; b && b.linearGradient ? x = "gradient" : b && b.radialGradient && (x = "pattern"); if (x) {
                    var B, t, z = b.linearGradient || b.radialGradient, f, F, H, A, u, p = ""; b = b.stops; var h, y = [], v = function () {
                        n = ['\x3cfill colors\x3d"' + y.join(",") +
                            '" opacity\x3d"', H, '" o:opacity2\x3d"', F, '" type\x3d"', x, '" ', p, 'focus\x3d"100%" method\x3d"any" /\x3e']; G(e.prepVML(n), null, null, c)
                    }; f = b[0]; h = b[b.length - 1]; 0 < f[0] && b.unshift([0, f[1]]); 1 > h[0] && b.push([1, h[1]]); l(b, function (g, b) { d.test(g[1]) ? (r = a.color(g[1]), B = r.get("rgb"), t = r.get("a")) : (B = g[1], t = 1); y.push(100 * g[0] + "% " + B); b ? (H = t, A = B) : (F = t, u = B) }); if ("fill" === m) if ("gradient" === x) m = z.x1 || z[0] || 0, b = z.y1 || z[1] || 0, f = z.x2 || z[2] || 0, z = z.y2 || z[3] || 0, p = 'angle\x3d"' + (90 - 180 * Math.atan((z - b) / (f - m)) / Math.PI) + '"',
                        v(); else { var g = z.r, C = 2 * g, D = 2 * g, I = z.cx, U = z.cy, L = c.radialReference, T, g = function () { L && (T = q.getBBox(), I += (L[0] - T.x) / T.width - .5, U += (L[1] - T.y) / T.height - .5, C *= L[2] / T.width, D *= L[2] / T.height); p = 'src\x3d"' + a.getOptions().global.VMLRadialGradientURL + '" size\x3d"' + C + "," + D + '" origin\x3d"0.5,0.5" position\x3d"' + I + "," + U + '" color2\x3d"' + u + '" '; v() }; q.added ? g() : q.onAdd = g; g = A } else g = B
                } else d.test(b) && "IMG" !== c.tagName ? (r = a.color(b), q[m + "-opacitySetter"](r.get("a"), m, c), g = r.get("rgb")) : (g = c.getElementsByTagName(m),
                    g.length && (g[0].opacity = 1, g[0].type = "solid"), g = b); return g
            }, prepVML: function (a) { var b = this.isIE8; a = a.join(""); b ? (a = a.replace("/\x3e", ' xmlns\x3d"urn:schemas-microsoft-com:vml" /\x3e'), a = -1 === a.indexOf('style\x3d"') ? a.replace("/\x3e", ' style\x3d"display:inline-block;behavior:url(#default#VML);" /\x3e') : a.replace('style\x3d"', 'style\x3d"display:inline-block;behavior:url(#default#VML);')) : a = a.replace("\x3c", "\x3chcv:"); return a }, text: z.prototype.html, path: function (a) {
                var b = { coordsize: "10 10" }; c(a) ? b.d =
                    a : y(a) && d(b, a); return this.createElement("shape").attr(b)
            }, circle: function (a, b, c) { var e = this.symbol("circle"); y(a) && (c = a.r, b = a.y, a = a.x); e.isCircle = !0; e.r = c; return e.attr({ x: a, y: b }) }, g: function (a) { var b; a && (b = { className: "highcharts-" + a, "class": "highcharts-" + a }); return this.createElement("div").attr(b) }, image: function (a, b, c, m, k) { var e = this.createElement("img").attr({ src: a }); 1 < arguments.length && e.attr({ x: b, y: c, width: m, height: k }); return e }, createElement: function (a) {
                return "rect" === a ? this.symbol(a) : z.prototype.createElement.call(this,
                    a)
            }, invertChild: function (a, c) { var e = this; c = c.style; var m = "IMG" === a.tagName && a.style; I(a, { flip: "x", left: b(c.width) - (m ? b(m.top) : 1), top: b(c.height) - (m ? b(m.left) : 1), rotation: -90 }); l(a.childNodes, function (b) { e.invertChild(b, a) }) }, symbols: {
                arc: function (a, b, c, m, k) {
                    var e = k.start, q = k.end, d = k.r || c || m; c = k.innerR; m = Math.cos(e); var r = Math.sin(e), g = Math.cos(q), B = Math.sin(q); if (0 === q - e) return ["x"]; e = ["wa", a - d, b - d, a + d, b + d, a + d * m, b + d * r, a + d * g, b + d * B]; k.open && !c && e.push("e", "M", a, b); e.push("at", a - c, b - c, a + c, b + c, a + c * g,
                        b + c * B, a + c * m, b + c * r, "x", "e"); e.isArc = !0; return e
                }, circle: function (a, b, c, m, k) { k && h(k.r) && (c = m = 2 * k.r); k && k.isCircle && (a -= c / 2, b -= m / 2); return ["wa", a, b, a + c, b + m, a + c, b + m / 2, a + c, b + m / 2, "e"] }, rect: function (a, b, c, m, k) { return z.prototype.symbols[h(k) && k.r ? "callout" : "square"].call(0, a, b, c, m, k) }
            }
        }, a.VMLRenderer = D = function () { this.init.apply(this, arguments) }, D.prototype = t(z.prototype, C), a.Renderer = D); z.prototype.measureSpanWidth = function (a, b) {
            var c = v.createElement("span"); a = v.createTextNode(a); c.appendChild(a); I(c,
                b); this.box.appendChild(c); b = c.offsetWidth; p(c); return b
        }
    })(L); (function (a) {
        function D() {
            var h = a.defaultOptions.global, l, u = h.useUTC, d = u ? "getUTC" : "get", c = u ? "setUTC" : "set"; a.Date = l = h.Date || p.Date; l.hcTimezoneOffset = u && h.timezoneOffset; l.hcGetTimezoneOffset = u && h.getTimezoneOffset; l.hcMakeTime = function (a, c, d, m, b, q) { var n; u ? (n = l.UTC.apply(0, arguments), n += I(n)) : n = (new l(a, c, f(d, 1), f(m, 0), f(b, 0), f(q, 0))).getTime(); return n }; G("Minutes Hours Day Date Month FullYear".split(" "), function (a) {
            l["hcGet" + a] = d +
                a
            }); G("Milliseconds Seconds Minutes Hours Date Month FullYear".split(" "), function (a) { l["hcSet" + a] = c + a })
        } var C = a.color, G = a.each, I = a.getTZOffset, h = a.merge, f = a.pick, p = a.win; a.defaultOptions = {
            colors: "#7cb5ec #434348 #90ed7d #f7a35c #8085e9 #f15c80 #e4d354 #2b908f #f45b5b #91e8e1".split(" "), symbols: ["circle", "diamond", "square", "triangle", "triangle-down"], lang: {
                loading: "Loading...", months: "January February March April May June July August September October November December".split(" "), shortMonths: "Jan Feb Mar Apr May Jun Jul Aug Sep Oct Nov Dec".split(" "),
                weekdays: "Sunday Monday Tuesday Wednesday Thursday Friday Saturday".split(" "), decimalPoint: ".", numericSymbols: "kMGTPE".split(""), resetZoom: "Reset zoom", resetZoomTitle: "Reset zoom level 1:1", thousandsSep: " "
            }, global: { useUTC: !0, VMLRadialGradientURL: "http://code.highcharts.com/5.0.6/gfx/vml-radial-gradient.png" }, chart: {
                borderRadius: 0, defaultSeriesType: "line", ignoreHiddenSeries: !0, spacing: [10, 10, 15, 10], resetZoomButton: { theme: { zIndex: 20 }, position: { align: "right", x: -10, y: 10 } }, width: null, height: null, borderColor: "#335cad",
                backgroundColor: "#ffffff", plotBorderColor: "#cccccc"
            }, title: { text: "Chart title", align: "center", margin: 15, widthAdjust: -44 }, subtitle: { text: "", align: "center", widthAdjust: -44 }, plotOptions: {}, labels: { style: { position: "absolute", color: "#333333" } }, legend: {
                enabled: !0, align: "center", layout: "horizontal", labelFormatter: function () { return this.name }, borderColor: "#999999", borderRadius: 0, navigation: { activeColor: "#003399", inactiveColor: "#cccccc" }, itemStyle: { color: "#333333", fontSize: "12px", fontWeight: "bold" }, itemHoverStyle: { color: "#000000" },
                itemHiddenStyle: { color: "#cccccc" }, shadow: !1, itemCheckboxStyle: { position: "absolute", width: "13px", height: "13px" }, squareSymbol: !0, symbolPadding: 5, verticalAlign: "bottom", x: 0, y: 0, title: { style: { fontWeight: "bold" } }
            }, loading: { labelStyle: { fontWeight: "bold", position: "relative", top: "45%" }, style: { position: "absolute", backgroundColor: "#ffffff", opacity: .5, textAlign: "center" } }, tooltip: {
                enabled: !0, animation: a.svg, borderRadius: 3, dateTimeLabelFormats: {
                    millisecond: "%A, %b %e, %H:%M:%S.%L", second: "%A, %b %e, %H:%M:%S",
                    minute: "%A, %b %e, %H:%M", hour: "%A, %b %e, %H:%M", day: "%A, %b %e, %Y", week: "Week from %A, %b %e, %Y", month: "%B %Y", year: "%Y"
                }, footerFormat: "", padding: 8, snap: a.isTouchDevice ? 25 : 10, backgroundColor: C("#f7f7f7").setOpacity(.85).get(), borderWidth: 1, headerFormat: '\x3cspan style\x3d"font-size: 10px"\x3e{point.key}\x3c/span\x3e\x3cbr/\x3e', pointFormat: '\x3cspan style\x3d"color:{point.color}"\x3e\u25cf\x3c/span\x3e {series.name}: \x3cb\x3e{point.y}\x3c/b\x3e\x3cbr/\x3e', shadow: !0, style: {
                    color: "#333333", cursor: "default",
                    fontSize: "12px", pointerEvents: "none", whiteSpace: "nowrap"
                }
            }, credits: { enabled: !0, href: "http://www.highcharts.com", position: { align: "right", x: -10, verticalAlign: "bottom", y: -5 }, style: { cursor: "pointer", color: "#999999", fontSize: "9px" }, text: "Highcharts.com" }
        }; a.setOptions = function (f) { a.defaultOptions = h(!0, a.defaultOptions, f); D(); return a.defaultOptions }; a.getOptions = function () { return a.defaultOptions }; a.defaultPlotOptions = a.defaultOptions.plotOptions; D()
    })(L); (function (a) {
        var D = a.arrayMax, C = a.arrayMin, G = a.defined,
        I = a.destroyObjectProperties, h = a.each, f = a.erase, p = a.merge, v = a.pick; a.PlotLineOrBand = function (a, f) { this.axis = a; f && (this.options = f, this.id = f.id) }; a.PlotLineOrBand.prototype = {
            render: function () {
                var a = this, f = a.axis, d = f.horiz, c = a.options, n = c.label, h = a.label, t = c.to, m = c.from, b = c.value, q = G(m) && G(t), z = G(b), F = a.svgElem, e = !F, r = [], x, A = c.color, k = v(c.zIndex, 0), w = c.events, r = { "class": "highcharts-plot-" + (q ? "band " : "line ") + (c.className || "") }, K = {}, J = f.chart.renderer, N = q ? "bands" : "lines", g = f.log2lin; f.isLog && (m = g(m), t =
                    g(t), b = g(b)); z ? (r = { stroke: A, "stroke-width": c.width }, c.dashStyle && (r.dashstyle = c.dashStyle)) : q && (A && (r.fill = A), c.borderWidth && (r.stroke = c.borderColor, r["stroke-width"] = c.borderWidth)); K.zIndex = k; N += "-" + k; (A = f[N]) || (f[N] = A = J.g("plot-" + N).attr(K).add()); e && (a.svgElem = F = J.path().attr(r).add(A)); if (z) r = f.getPlotLinePath(b, F.strokeWidth()); else if (q) r = f.getPlotBandPath(m, t, c); else return; if (e && r && r.length) { if (F.attr({ d: r }), w) for (x in c = function (g) { F.on(g, function (b) { w[g].apply(a, [b]) }) }, w) c(x) } else F &&
                        (r ? (F.show(), F.animate({ d: r })) : (F.hide(), h && (a.label = h = h.destroy()))); n && G(n.text) && r && r.length && 0 < f.width && 0 < f.height && !r.flat ? (n = p({ align: d && q && "center", x: d ? !q && 4 : 10, verticalAlign: !d && q && "middle", y: d ? q ? 16 : 10 : q ? 6 : -4, rotation: d && !q && 90 }, n), this.renderLabel(n, r, q, k)) : h && h.hide(); return a
            }, renderLabel: function (a, f, d, c) {
                var n = this.label, l = this.axis.chart.renderer; n || (n = { align: a.textAlign || a.align, rotation: a.rotation, "class": "highcharts-plot-" + (d ? "band" : "line") + "-label " + (a.className || "") }, n.zIndex = c,
                    this.label = n = l.text(a.text, 0, 0, a.useHTML).attr(n).add(), n.css(a.style)); c = [f[1], f[4], d ? f[6] : f[1]]; f = [f[2], f[5], d ? f[7] : f[2]]; d = C(c); l = C(f); n.align(a, !1, { x: d, y: l, width: D(c) - d, height: D(f) - l }); n.show()
            }, destroy: function () { f(this.axis.plotLinesAndBands, this); delete this.axis; I(this) }
        }; a.AxisPlotLineOrBandExtension = {
            getPlotBandPath: function (a, f) {
                f = this.getPlotLinePath(f, null, null, !0); (a = this.getPlotLinePath(a, null, null, !0)) && f ? (a.flat = a.toString() === f.toString(), a.push(f[4], f[5], f[1], f[2], "z")) : a = null;
                return a
            }, addPlotBand: function (a) { return this.addPlotBandOrLine(a, "plotBands") }, addPlotLine: function (a) { return this.addPlotBandOrLine(a, "plotLines") }, addPlotBandOrLine: function (f, h) { var d = (new a.PlotLineOrBand(this, f)).render(), c = this.userOptions; d && (h && (c[h] = c[h] || [], c[h].push(f)), this.plotLinesAndBands.push(d)); return d }, removePlotBandOrLine: function (a) {
                for (var l = this.plotLinesAndBands, d = this.options, c = this.userOptions, n = l.length; n--;)l[n].id === a && l[n].destroy(); h([d.plotLines || [], c.plotLines ||
                    [], d.plotBands || [], c.plotBands || []], function (c) { for (n = c.length; n--;)c[n].id === a && f(c, c[n]) })
            }
        }
    })(L); (function (a) {
        var D = a.correctFloat, C = a.defined, G = a.destroyObjectProperties, I = a.isNumber, h = a.merge, f = a.pick, p = a.deg2rad; a.Tick = function (a, f, h, d) { this.axis = a; this.pos = f; this.type = h || ""; this.isNew = !0; h || d || this.addLabel() }; a.Tick.prototype = {
            addLabel: function () {
                var a = this.axis, l = a.options, p = a.chart, d = a.categories, c = a.names, n = this.pos, y = l.labels, t = a.tickPositions, m = n === t[0], b = n === t[t.length - 1], c = d ? f(d[n],
                    c[n], n) : n, d = this.label, t = t.info, q; a.isDatetimeAxis && t && (q = l.dateTimeLabelFormats[t.higherRanks[n] || t.unitName]); this.isFirst = m; this.isLast = b; l = a.labelFormatter.call({ axis: a, chart: p, isFirst: m, isLast: b, dateTimeLabelFormat: q, value: a.isLog ? D(a.lin2log(c)) : c }); C(d) ? d && d.attr({ text: l }) : (this.labelLength = (this.label = d = C(l) && y.enabled ? p.renderer.text(l, 0, 0, y.useHTML).css(h(y.style)).add(a.labelGroup) : null) && d.getBBox().width, this.rotation = 0)
            }, getLabelSize: function () {
                return this.label ? this.label.getBBox()[this.axis.horiz ?
                    "height" : "width"] : 0
            }, handleOverflow: function (a) {
                var l = this.axis, h = a.x, d = l.chart.chartWidth, c = l.chart.spacing, n = f(l.labelLeft, Math.min(l.pos, c[3])), c = f(l.labelRight, Math.max(l.pos + l.len, d - c[1])), y = this.label, t = this.rotation, m = { left: 0, center: .5, right: 1 }[l.labelAlign], b = y.getBBox().width, q = l.getSlotWidth(), z = q, F = 1, e, r = {}; if (t) 0 > t && h - m * b < n ? e = Math.round(h / Math.cos(t * p) - n) : 0 < t && h + m * b > c && (e = Math.round((d - h) / Math.cos(t * p))); else if (d = h + (1 - m) * b, h - m * b < n ? z = a.x + z * (1 - m) - n : d > c && (z = c - a.x + z * m, F = -1), z = Math.min(q,
                    z), z < q && "center" === l.labelAlign && (a.x += F * (q - z - m * (q - Math.min(b, z)))), b > z || l.autoRotation && (y.styles || {}).width) e = z; e && (r.width = e, (l.options.labels.style || {}).textOverflow || (r.textOverflow = "ellipsis"), y.css(r))
            }, getPosition: function (a, f, h, d) {
                var c = this.axis, n = c.chart, l = d && n.oldChartHeight || n.chartHeight; return {
                    x: a ? c.translate(f + h, null, null, d) + c.transB : c.left + c.offset + (c.opposite ? (d && n.oldChartWidth || n.chartWidth) - c.right - c.left : 0), y: a ? l - c.bottom + c.offset - (c.opposite ? c.height : 0) : l - c.translate(f + h, null,
                        null, d) - c.transB
                }
            }, getLabelPosition: function (a, f, h, d, c, n, y, t) { var m = this.axis, b = m.transA, q = m.reversed, z = m.staggerLines, l = m.tickRotCorr || { x: 0, y: 0 }, e = c.y; C(e) || (e = 0 === m.side ? h.rotation ? -8 : -h.getBBox().height : 2 === m.side ? l.y + 8 : Math.cos(h.rotation * p) * (l.y - h.getBBox(!1, 0).height / 2)); a = a + c.x + l.x - (n && d ? n * b * (q ? -1 : 1) : 0); f = f + e - (n && !d ? n * b * (q ? 1 : -1) : 0); z && (h = y / (t || 1) % z, m.opposite && (h = z - h - 1), f += m.labelOffset / z * h); return { x: a, y: Math.round(f) } }, getMarkPath: function (a, f, h, d, c, n) {
                return n.crispLine(["M", a, f, "L", a + (c ?
                    0 : -h), f + (c ? h : 0)], d)
            }, render: function (a, l, h) {
                var d = this.axis, c = d.options, n = d.chart.renderer, p = d.horiz, t = this.type, m = this.label, b = this.pos, q = c.labels, z = this.gridLine, F = t ? t + "Tick" : "tick", e = d.tickSize(F), r = this.mark, x = !r, A = q.step, k = {}, w = !0, K = d.tickmarkOffset, J = this.getPosition(p, b, K, l), u = J.x, J = J.y, g = p && u === d.pos + d.len || !p && J === d.pos ? -1 : 1, B = t ? t + "Grid" : "grid", S = c[B + "LineWidth"], M = c[B + "LineColor"], v = c[B + "LineDashStyle"], B = f(c[F + "Width"], !t && d.isXAxis ? 1 : 0), F = c[F + "Color"]; h = f(h, 1); this.isActive = !0; z || (k.stroke =
                    M, k["stroke-width"] = S, v && (k.dashstyle = v), t || (k.zIndex = 1), l && (k.opacity = 0), this.gridLine = z = n.path().attr(k).addClass("highcharts-" + (t ? t + "-" : "") + "grid-line").add(d.gridGroup)); if (!l && z && (b = d.getPlotLinePath(b + K, z.strokeWidth() * g, l, !0))) z[this.isNew ? "attr" : "animate"]({ d: b, opacity: h }); e && (d.opposite && (e[0] = -e[0]), x && (this.mark = r = n.path().addClass("highcharts-" + (t ? t + "-" : "") + "tick").add(d.axisGroup), r.attr({ stroke: F, "stroke-width": B })), r[x ? "attr" : "animate"]({
                        d: this.getMarkPath(u, J, e[0], r.strokeWidth() *
                            g, p, n), opacity: h
                    })); m && I(u) && (m.xy = J = this.getLabelPosition(u, J, m, p, q, K, a, A), this.isFirst && !this.isLast && !f(c.showFirstLabel, 1) || this.isLast && !this.isFirst && !f(c.showLastLabel, 1) ? w = !1 : !p || d.isRadial || q.step || q.rotation || l || 0 === h || this.handleOverflow(J), A && a % A && (w = !1), w && I(J.y) ? (J.opacity = h, m[this.isNew ? "attr" : "animate"](J)) : m.attr("y", -9999), this.isNew = !1)
            }, destroy: function () { G(this, this.axis) }
        }
    })(L); (function (a) {
        var D = a.addEvent, C = a.animObject, G = a.arrayMax, I = a.arrayMin, h = a.AxisPlotLineOrBandExtension,
        f = a.color, p = a.correctFloat, v = a.defaultOptions, l = a.defined, u = a.deg2rad, d = a.destroyObjectProperties, c = a.each, n = a.extend, y = a.fireEvent, t = a.format, m = a.getMagnitude, b = a.grep, q = a.inArray, z = a.isArray, F = a.isNumber, e = a.isString, r = a.merge, x = a.normalizeTickInterval, A = a.pick, k = a.PlotLineOrBand, w = a.removeEvent, K = a.splat, J = a.syncTimeout, N = a.Tick; a.Axis = function () { this.init.apply(this, arguments) }; a.Axis.prototype = {
            defaultOptions: {
                dateTimeLabelFormats: {
                    millisecond: "%H:%M:%S.%L", second: "%H:%M:%S", minute: "%H:%M", hour: "%H:%M",
                    day: "%e. %b", week: "%e. %b", month: "%b '%y", year: "%Y"
                }, endOnTick: !1, labels: { enabled: !0, style: { color: "#666666", cursor: "default", fontSize: "11px" }, x: 0 }, minPadding: .01, maxPadding: .01, minorTickLength: 2, minorTickPosition: "outside", startOfWeek: 1, startOnTick: !1, tickLength: 10, tickmarkPlacement: "between", tickPixelInterval: 100, tickPosition: "outside", title: { align: "middle", style: { color: "#666666" } }, type: "linear", minorGridLineColor: "#f2f2f2", minorGridLineWidth: 1, minorTickColor: "#999999", lineColor: "#ccd6eb", lineWidth: 1,
                gridLineColor: "#e6e6e6", tickColor: "#ccd6eb"
            }, defaultYAxisOptions: { endOnTick: !0, tickPixelInterval: 72, showLastLabel: !0, labels: { x: -8 }, maxPadding: .05, minPadding: .05, startOnTick: !0, title: { rotation: 270, text: "Values" }, stackLabels: { enabled: !1, formatter: function () { return a.numberFormat(this.total, -1) }, style: { fontSize: "11px", fontWeight: "bold", color: "#000000", textOutline: "1px contrast" } }, gridLineWidth: 1, lineWidth: 0 }, defaultLeftAxisOptions: { labels: { x: -15 }, title: { rotation: 270 } }, defaultRightAxisOptions: {
                labels: { x: 15 },
                title: { rotation: 90 }
            }, defaultBottomAxisOptions: { labels: { autoRotation: [-45], x: 0 }, title: { rotation: 0 } }, defaultTopAxisOptions: { labels: { autoRotation: [-45], x: 0 }, title: { rotation: 0 } }, init: function (a, b) {
                var g = b.isX; this.chart = a; this.horiz = a.inverted ? !g : g; this.isXAxis = g; this.coll = this.coll || (g ? "xAxis" : "yAxis"); this.opposite = b.opposite; this.side = b.side || (this.horiz ? this.opposite ? 0 : 2 : this.opposite ? 1 : 3); this.setOptions(b); var c = this.options, e = c.type; this.labelFormatter = c.labels.formatter || this.defaultLabelFormatter;
                this.userOptions = b; this.minPixelPadding = 0; this.reversed = c.reversed; this.visible = !1 !== c.visible; this.zoomEnabled = !1 !== c.zoomEnabled; this.hasNames = "category" === e || !0 === c.categories; this.categories = c.categories || this.hasNames; this.names = this.names || []; this.isLog = "logarithmic" === e; this.isDatetimeAxis = "datetime" === e; this.isLinked = l(c.linkedTo); this.ticks = {}; this.labelEdge = []; this.minorTicks = {}; this.plotLinesAndBands = []; this.alternateBands = {}; this.len = 0; this.minRange = this.userMinRange = c.minRange || c.maxZoom;
                this.range = c.range; this.offset = c.offset || 0; this.stacks = {}; this.oldStacks = {}; this.stacksTouched = 0; this.min = this.max = null; this.crosshair = A(c.crosshair, K(a.options.tooltip.crosshairs)[g ? 0 : 1], !1); var k; b = this.options.events; -1 === q(this, a.axes) && (g ? a.axes.splice(a.xAxis.length, 0, this) : a.axes.push(this), a[this.coll].push(this)); this.series = this.series || []; a.inverted && g && void 0 === this.reversed && (this.reversed = !0); this.removePlotLine = this.removePlotBand = this.removePlotBandOrLine; for (k in b) D(this, k, b[k]);
                this.isLog && (this.val2lin = this.log2lin, this.lin2val = this.lin2log)
            }, setOptions: function (a) { this.options = r(this.defaultOptions, "yAxis" === this.coll && this.defaultYAxisOptions, [this.defaultTopAxisOptions, this.defaultRightAxisOptions, this.defaultBottomAxisOptions, this.defaultLeftAxisOptions][this.side], r(v[this.coll], a)) }, defaultLabelFormatter: function () {
                var g = this.axis, b = this.value, c = g.categories, e = this.dateTimeLabelFormat, k = v.lang, m = k.numericSymbols, k = k.numericSymbolMagnitude || 1E3, q = m && m.length, d, r = g.options.labels.format,
                g = g.isLog ? b : g.tickInterval; if (r) d = t(r, this); else if (c) d = b; else if (e) d = a.dateFormat(e, b); else if (q && 1E3 <= g) for (; q-- && void 0 === d;)c = Math.pow(k, q + 1), g >= c && 0 === 10 * b % c && null !== m[q] && 0 !== b && (d = a.numberFormat(b / c, -1) + m[q]); void 0 === d && (d = 1E4 <= Math.abs(b) ? a.numberFormat(b, -1) : a.numberFormat(b, -1, void 0, "")); return d
            }, getSeriesExtremes: function () {
                var a = this, e = a.chart; a.hasVisibleSeries = !1; a.dataMin = a.dataMax = a.threshold = null; a.softThreshold = !a.isXAxis; a.buildStacks && a.buildStacks(); c(a.series, function (g) {
                    if (g.visible ||
                        !e.options.chart.ignoreHiddenSeries) {
                            var c = g.options, k = c.threshold, B; a.hasVisibleSeries = !0; a.isLog && 0 >= k && (k = null); if (a.isXAxis) c = g.xData, c.length && (g = I(c), F(g) || g instanceof Date || (c = b(c, function (a) { return F(a) }), g = I(c)), a.dataMin = Math.min(A(a.dataMin, c[0]), g), a.dataMax = Math.max(A(a.dataMax, c[0]), G(c))); else if (g.getExtremes(), B = g.dataMax, g = g.dataMin, l(g) && l(B) && (a.dataMin = Math.min(A(a.dataMin, g), g), a.dataMax = Math.max(A(a.dataMax, B), B)), l(k) && (a.threshold = k), !c.softThreshold || a.isLog) a.softThreshold =
                                !1
                    }
                })
            }, translate: function (a, b, c, e, k, m) { var g = this.linkedParent || this, B = 1, q = 0, d = e ? g.oldTransA : g.transA; e = e ? g.oldMin : g.min; var r = g.minPixelPadding; k = (g.isOrdinal || g.isBroken || g.isLog && k) && g.lin2val; d || (d = g.transA); c && (B *= -1, q = g.len); g.reversed && (B *= -1, q -= B * (g.sector || g.len)); b ? (a = (a * B + q - r) / d + e, k && (a = g.lin2val(a))) : (k && (a = g.val2lin(a)), a = B * (a - e) * d + q + B * r + (F(m) ? d * m : 0)); return a }, toPixels: function (a, b) { return this.translate(a, !1, !this.horiz, null, !0) + (b ? 0 : this.pos) }, toValue: function (a, b) {
                return this.translate(a -
                    (b ? 0 : this.pos), !0, !this.horiz, null, !0)
            }, getPlotLinePath: function (a, b, c, e, k) {
                var g = this.chart, B = this.left, m = this.top, q, d, r = c && g.oldChartHeight || g.chartHeight, n = c && g.oldChartWidth || g.chartWidth, f; q = this.transB; var w = function (a, g, b) { if (a < g || a > b) e ? a = Math.min(Math.max(g, a), b) : f = !0; return a }; k = A(k, this.translate(a, null, null, c)); a = c = Math.round(k + q); q = d = Math.round(r - k - q); F(k) ? this.horiz ? (q = m, d = r - this.bottom, a = c = w(a, B, B + this.width)) : (a = B, c = n - this.right, q = d = w(q, m, m + this.height)) : f = !0; return f && !e ? null : g.renderer.crispLine(["M",
                    a, q, "L", c, d], b || 1)
            }, getLinearTickPositions: function (a, b, c) { var g, k = p(Math.floor(b / a) * a), e = p(Math.ceil(c / a) * a), B = []; if (b === c && F(b)) return [b]; for (b = k; b <= e;) { B.push(b); b = p(b + a); if (b === g) break; g = b } return B }, getMinorTickPositions: function () {
                var a = this.options, b = this.tickPositions, c = this.minorTickInterval, k = [], e, m = this.pointRangePadding || 0; e = this.min - m; var m = this.max + m, q = m - e; if (q && q / c < this.len / 3) if (this.isLog) for (m = b.length, e = 1; e < m; e++)k = k.concat(this.getLogTickPositions(c, b[e - 1], b[e], !0)); else if (this.isDatetimeAxis &&
                    "auto" === a.minorTickInterval) k = k.concat(this.getTimeTicks(this.normalizeTimeTickInterval(c), e, m, a.startOfWeek)); else for (b = e + (b[0] - e) % c; b <= m && b !== k[0]; b += c)k.push(b); 0 !== k.length && this.trimTicks(k, a.startOnTick, a.endOnTick); return k
            }, adjustForMinRange: function () {
                var a = this.options, b = this.min, k = this.max, e, m = this.dataMax - this.dataMin >= this.minRange, q, d, r, f, n, w; this.isXAxis && void 0 === this.minRange && !this.isLog && (l(a.min) || l(a.max) ? this.minRange = null : (c(this.series, function (a) {
                    f = a.xData; for (d = n = a.xIncrement ?
                        1 : f.length - 1; 0 < d; d--)if (r = f[d] - f[d - 1], void 0 === q || r < q) q = r
                }), this.minRange = Math.min(5 * q, this.dataMax - this.dataMin))); k - b < this.minRange && (w = this.minRange, e = (w - k + b) / 2, e = [b - e, A(a.min, b - e)], m && (e[2] = this.isLog ? this.log2lin(this.dataMin) : this.dataMin), b = G(e), k = [b + w, A(a.max, b + w)], m && (k[2] = this.isLog ? this.log2lin(this.dataMax) : this.dataMax), k = I(k), k - b < w && (e[0] = k - w, e[1] = A(a.min, k - w), b = G(e))); this.min = b; this.max = k
            }, getClosest: function () {
                var a; this.categories ? a = 1 : c(this.series, function (b) {
                    var g = b.closestPointRange,
                    c = b.visible || !b.chart.options.chart.ignoreHiddenSeries; !b.noSharedTooltip && l(g) && c && (a = l(a) ? Math.min(a, g) : g)
                }); return a
            }, nameToX: function (a) { var b = z(this.categories), g = b ? this.categories : this.names, c = a.options.x, k; a.series.requireSorting = !1; l(c) || (c = !1 === this.options.uniqueNames ? a.series.autoIncrement() : q(a.name, g)); -1 === c ? b || (k = g.length) : k = c; this.names[k] = a.name; return k }, updateNames: function () {
                var a = this; 0 < this.names.length && (this.names.length = 0, this.minRange = void 0, c(this.series || [], function (b) {
                b.xIncrement =
                    null; if (!b.points || b.isDirtyData) b.processData(), b.generatePoints(); c(b.points, function (g, c) { var k; g.options && void 0 === g.options.x && (k = a.nameToX(g), k !== g.x && (g.x = k, b.xData[c] = k)) })
                }))
            }, setAxisTranslation: function (a) {
                var b = this, g = b.max - b.min, k = b.axisPointRange || 0, m, q = 0, d = 0, r = b.linkedParent, f = !!b.categories, n = b.transA, w = b.isXAxis; if (w || f || k) m = b.getClosest(), r ? (q = r.minPointOffset, d = r.pointRangePadding) : c(b.series, function (a) {
                    var g = f ? 1 : w ? A(a.options.pointRange, m, 0) : b.axisPointRange || 0; a = a.options.pointPlacement;
                    k = Math.max(k, g); b.single || (q = Math.max(q, e(a) ? 0 : g / 2), d = Math.max(d, "on" === a ? 0 : g))
                }), r = b.ordinalSlope && m ? b.ordinalSlope / m : 1, b.minPointOffset = q *= r, b.pointRangePadding = d *= r, b.pointRange = Math.min(k, g), w && (b.closestPointRange = m); a && (b.oldTransA = n); b.translationSlope = b.transA = n = b.len / (g + d || 1); b.transB = b.horiz ? b.left : b.bottom; b.minPixelPadding = n * q
            }, minFromRange: function () { return this.max - this.range }, setTickInterval: function (b) {
                var g = this, k = g.chart, e = g.options, q = g.isLog, d = g.log2lin, r = g.isDatetimeAxis, f = g.isXAxis,
                n = g.isLinked, w = e.maxPadding, t = e.minPadding, z = e.tickInterval, h = e.tickPixelInterval, K = g.categories, J = g.threshold, u = g.softThreshold, N, v, C, D; r || K || n || this.getTickAmount(); C = A(g.userMin, e.min); D = A(g.userMax, e.max); n ? (g.linkedParent = k[g.coll][e.linkedTo], k = g.linkedParent.getExtremes(), g.min = A(k.min, k.dataMin), g.max = A(k.max, k.dataMax), e.type !== g.linkedParent.options.type && a.error(11, 1)) : (!u && l(J) && (g.dataMin >= J ? (N = J, t = 0) : g.dataMax <= J && (v = J, w = 0)), g.min = A(C, N, g.dataMin), g.max = A(D, v, g.dataMax)); q && (!b && 0 >=
                    Math.min(g.min, A(g.dataMin, g.min)) && a.error(10, 1), g.min = p(d(g.min), 15), g.max = p(d(g.max), 15)); g.range && l(g.max) && (g.userMin = g.min = C = Math.max(g.min, g.minFromRange()), g.userMax = D = g.max, g.range = null); y(g, "foundExtremes"); g.beforePadding && g.beforePadding(); g.adjustForMinRange(); !(K || g.axisPointRange || g.usePercentage || n) && l(g.min) && l(g.max) && (d = g.max - g.min) && (!l(C) && t && (g.min -= d * t), !l(D) && w && (g.max += d * w)); F(e.floor) ? g.min = Math.max(g.min, e.floor) : F(e.softMin) && (g.min = Math.min(g.min, e.softMin)); F(e.ceiling) ?
                        g.max = Math.min(g.max, e.ceiling) : F(e.softMax) && (g.max = Math.max(g.max, e.softMax)); u && l(g.dataMin) && (J = J || 0, !l(C) && g.min < J && g.dataMin >= J ? g.min = J : !l(D) && g.max > J && g.dataMax <= J && (g.max = J)); g.tickInterval = g.min === g.max || void 0 === g.min || void 0 === g.max ? 1 : n && !z && h === g.linkedParent.options.tickPixelInterval ? z = g.linkedParent.tickInterval : A(z, this.tickAmount ? (g.max - g.min) / Math.max(this.tickAmount - 1, 1) : void 0, K ? 1 : (g.max - g.min) * h / Math.max(g.len, h)); f && !b && c(g.series, function (a) {
                            a.processData(g.min !== g.oldMin || g.max !==
                                g.oldMax)
                        }); g.setAxisTranslation(!0); g.beforeSetTickPositions && g.beforeSetTickPositions(); g.postProcessTickInterval && (g.tickInterval = g.postProcessTickInterval(g.tickInterval)); g.pointRange && !z && (g.tickInterval = Math.max(g.pointRange, g.tickInterval)); b = A(e.minTickInterval, g.isDatetimeAxis && g.closestPointRange); !z && g.tickInterval < b && (g.tickInterval = b); r || q || z || (g.tickInterval = x(g.tickInterval, null, m(g.tickInterval), A(e.allowDecimals, !(.5 < g.tickInterval && 5 > g.tickInterval && 1E3 < g.max && 9999 > g.max)), !!this.tickAmount));
                this.tickAmount || (g.tickInterval = g.unsquish()); this.setTickPositions()
            }, setTickPositions: function () {
                var a = this.options, b, c = a.tickPositions, k = a.tickPositioner, e = a.startOnTick, m = a.endOnTick, q; this.tickmarkOffset = this.categories && "between" === a.tickmarkPlacement && 1 === this.tickInterval ? .5 : 0; this.minorTickInterval = "auto" === a.minorTickInterval && this.tickInterval ? this.tickInterval / 5 : a.minorTickInterval; this.tickPositions = b = c && c.slice(); !b && (b = this.isDatetimeAxis ? this.getTimeTicks(this.normalizeTimeTickInterval(this.tickInterval,
                    a.units), this.min, this.max, a.startOfWeek, this.ordinalPositions, this.closestPointRange, !0) : this.isLog ? this.getLogTickPositions(this.tickInterval, this.min, this.max) : this.getLinearTickPositions(this.tickInterval, this.min, this.max), b.length > this.len && (b = [b[0], b.pop()]), this.tickPositions = b, k && (k = k.apply(this, [this.min, this.max]))) && (this.tickPositions = b = k); this.isLinked || (this.trimTicks(b, e, m), this.min === this.max && l(this.min) && !this.tickAmount && (q = !0, this.min -= .5, this.max += .5), this.single = q, c || k || this.adjustTickAmount())
            },
            trimTicks: function (a, b, c) { var g = a[0], k = a[a.length - 1], e = this.minPointOffset || 0; if (b) this.min = g; else for (; this.min - e > a[0];)a.shift(); if (c) this.max = k; else for (; this.max + e < a[a.length - 1];)a.pop(); 0 === a.length && l(g) && a.push((k + g) / 2) }, alignToOthers: function () { var a = {}, b, k = this.options; !1 === this.chart.options.chart.alignTicks || !1 === k.alignTicks || this.isLog || c(this.chart[this.coll], function (g) { var c = g.options, c = [g.horiz ? c.left : c.top, c.width, c.height, c.pane].join(); g.series.length && (a[c] ? b = !0 : a[c] = 1) }); return b },
            getTickAmount: function () { var a = this.options, b = a.tickAmount, c = a.tickPixelInterval; !l(a.tickInterval) && this.len < c && !this.isRadial && !this.isLog && a.startOnTick && a.endOnTick && (b = 2); !b && this.alignToOthers() && (b = Math.ceil(this.len / c) + 1); 4 > b && (this.finalTickAmt = b, b = 5); this.tickAmount = b }, adjustTickAmount: function () {
                var a = this.tickInterval, b = this.tickPositions, c = this.tickAmount, k = this.finalTickAmt, e = b && b.length; if (e < c) {
                    for (; b.length < c;)b.push(p(b[b.length - 1] + a)); this.transA *= (e - 1) / (c - 1); this.max = b[b.length -
                        1]
                } else e > c && (this.tickInterval *= 2, this.setTickPositions()); if (l(k)) { for (a = c = b.length; a--;)(3 === k && 1 === a % 2 || 2 >= k && 0 < a && a < c - 1) && b.splice(a, 1); this.finalTickAmt = void 0 }
            }, setScale: function () {
                var a, b; this.oldMin = this.min; this.oldMax = this.max; this.oldAxisLength = this.len; this.setAxisSize(); b = this.len !== this.oldAxisLength; c(this.series, function (b) { if (b.isDirtyData || b.isDirty || b.xAxis.isDirty) a = !0 }); b || a || this.isLinked || this.forceRedraw || this.userMin !== this.oldUserMin || this.userMax !== this.oldUserMax || this.alignToOthers() ?
                    (this.resetStacks && this.resetStacks(), this.forceRedraw = !1, this.getSeriesExtremes(), this.setTickInterval(), this.oldUserMin = this.userMin, this.oldUserMax = this.userMax, this.isDirty || (this.isDirty = b || this.min !== this.oldMin || this.max !== this.oldMax)) : this.cleanStacks && this.cleanStacks()
            }, setExtremes: function (a, b, k, e, m) { var g = this, q = g.chart; k = A(k, !0); c(g.series, function (a) { delete a.kdTree }); m = n(m, { min: a, max: b }); y(g, "setExtremes", m, function () { g.userMin = a; g.userMax = b; g.eventArgs = m; k && q.redraw(e) }) }, zoom: function (a,
                b) { var g = this.dataMin, c = this.dataMax, k = this.options, e = Math.min(g, A(k.min, g)), k = Math.max(c, A(k.max, c)); if (a !== this.min || b !== this.max) this.allowZoomOutside || (l(g) && (a < e && (a = e), a > k && (a = k)), l(c) && (b < e && (b = e), b > k && (b = k))), this.displayBtn = void 0 !== a || void 0 !== b, this.setExtremes(a, b, !1, void 0, { trigger: "zoom" }); return !0 }, setAxisSize: function () {
                    var a = this.chart, b = this.options, c = b.offsetLeft || 0, k = this.horiz, e = A(b.width, a.plotWidth - c + (b.offsetRight || 0)), m = A(b.height, a.plotHeight), q = A(b.top, a.plotTop), b = A(b.left,
                        a.plotLeft + c), c = /%$/; c.test(m) && (m = Math.round(parseFloat(m) / 100 * a.plotHeight)); c.test(q) && (q = Math.round(parseFloat(q) / 100 * a.plotHeight + a.plotTop)); this.left = b; this.top = q; this.width = e; this.height = m; this.bottom = a.chartHeight - m - q; this.right = a.chartWidth - e - b; this.len = Math.max(k ? e : m, 0); this.pos = k ? b : q
                }, getExtremes: function () { var a = this.isLog, b = this.lin2log; return { min: a ? p(b(this.min)) : this.min, max: a ? p(b(this.max)) : this.max, dataMin: this.dataMin, dataMax: this.dataMax, userMin: this.userMin, userMax: this.userMax } },
            getThreshold: function (a) { var b = this.isLog, g = this.lin2log, c = b ? g(this.min) : this.min, b = b ? g(this.max) : this.max; null === a ? a = c : c > a ? a = c : b < a && (a = b); return this.translate(a, 0, 1, 0, 1) }, autoLabelAlign: function (a) { a = (A(a, 0) - 90 * this.side + 720) % 360; return 15 < a && 165 > a ? "right" : 195 < a && 345 > a ? "left" : "center" }, tickSize: function (a) { var b = this.options, g = b[a + "Length"], c = A(b[a + "Width"], "tick" === a && this.isXAxis ? 1 : 0); if (c && g) return "inside" === b[a + "Position"] && (g = -g), [g, c] }, labelMetrics: function () {
                return this.chart.renderer.fontMetrics(this.options.labels.style &&
                    this.options.labels.style.fontSize, this.ticks[0] && this.ticks[0].label)
            }, unsquish: function () {
                var a = this.options.labels, b = this.horiz, k = this.tickInterval, e = k, m = this.len / (((this.categories ? 1 : 0) + this.max - this.min) / k), q, d = a.rotation, r = this.labelMetrics(), f, n = Number.MAX_VALUE, w, t = function (a) { a /= m || 1; a = 1 < a ? Math.ceil(a) : 1; return a * k }; b ? (w = !a.staggerLines && !a.step && (l(d) ? [d] : m < A(a.autoRotationLimit, 80) && a.autoRotation)) && c(w, function (a) {
                    var b; if (a === d || a && -90 <= a && 90 >= a) f = t(Math.abs(r.h / Math.sin(u * a))), b = f +
                        Math.abs(a / 360), b < n && (n = b, q = a, e = f)
                }) : a.step || (e = t(r.h)); this.autoRotation = w; this.labelRotation = A(q, d); return e
            }, getSlotWidth: function () { var a = this.chart, b = this.horiz, c = this.options.labels, k = Math.max(this.tickPositions.length - (this.categories ? 0 : 1), 1), e = a.margin[3]; return b && 2 > (c.step || 0) && !c.rotation && (this.staggerLines || 1) * a.plotWidth / k || !b && (e && e - a.spacing[3] || .33 * a.chartWidth) }, renderUnsquish: function () {
                var a = this.chart, b = a.renderer, k = this.tickPositions, m = this.ticks, q = this.options.labels, d = this.horiz,
                f = this.getSlotWidth(), n = Math.max(1, Math.round(f - 2 * (q.padding || 5))), w = {}, t = this.labelMetrics(), z = q.style && q.style.textOverflow, h, l = 0, x, F; e(q.rotation) || (w.rotation = q.rotation || 0); c(k, function (a) { (a = m[a]) && a.labelLength > l && (l = a.labelLength) }); this.maxLabelLength = l; if (this.autoRotation) l > n && l > t.h ? w.rotation = this.labelRotation : this.labelRotation = 0; else if (f && (h = { width: n + "px" }, !z)) for (h.textOverflow = "clip", x = k.length; !d && x--;)if (F = k[x], n = m[F].label) n.styles && "ellipsis" === n.styles.textOverflow ? n.css({ textOverflow: "clip" }) :
                    m[F].labelLength > f && n.css({ width: f + "px" }), n.getBBox().height > this.len / k.length - (t.h - t.f) && (n.specCss = { textOverflow: "ellipsis" }); w.rotation && (h = { width: (l > .5 * a.chartHeight ? .33 * a.chartHeight : a.chartHeight) + "px" }, z || (h.textOverflow = "ellipsis")); if (this.labelAlign = q.align || this.autoLabelAlign(this.labelRotation)) w.align = this.labelAlign; c(k, function (a) { var b = (a = m[a]) && a.label; b && (b.attr(w), h && b.css(r(h, b.specCss)), delete b.specCss, a.rotation = w.rotation) }); this.tickRotCorr = b.rotCorr(t.b, this.labelRotation ||
                        0, 0 !== this.side)
            }, hasData: function () { return this.hasVisibleSeries || l(this.min) && l(this.max) && !!this.tickPositions }, addTitle: function (a) {
                var b = this.chart.renderer, g = this.horiz, c = this.opposite, k = this.options.title, e; this.axisTitle || ((e = k.textAlign) || (e = (g ? { low: "left", middle: "center", high: "right" } : { low: c ? "right" : "left", middle: "center", high: c ? "left" : "right" })[k.align]), this.axisTitle = b.text(k.text, 0, 0, k.useHTML).attr({ zIndex: 7, rotation: k.rotation || 0, align: e }).addClass("highcharts-axis-title").css(k.style).add(this.axisGroup),
                    this.axisTitle.isNew = !0); this.axisTitle[a ? "show" : "hide"](!0)
            }, getOffset: function () {
                var a = this, b = a.chart, k = b.renderer, e = a.options, m = a.tickPositions, q = a.ticks, d = a.horiz, r = a.side, n = b.inverted ? [1, 0, 3, 2][r] : r, w, f, t = 0, z, h = 0, x = e.title, F = e.labels, p = 0, K = b.axisOffset, b = b.clipOffset, J = [-1, 1, 1, -1][r], u, y = e.className, v = a.axisParent, C = this.tickSize("tick"); w = a.hasData(); a.showAxis = f = w || A(e.showEmpty, !0); a.staggerLines = a.horiz && F.staggerLines; a.axisGroup || (a.gridGroup = k.g("grid").attr({ zIndex: e.gridZIndex || 1 }).addClass("highcharts-" +
                    this.coll.toLowerCase() + "-grid " + (y || "")).add(v), a.axisGroup = k.g("axis").attr({ zIndex: e.zIndex || 2 }).addClass("highcharts-" + this.coll.toLowerCase() + " " + (y || "")).add(v), a.labelGroup = k.g("axis-labels").attr({ zIndex: F.zIndex || 7 }).addClass("highcharts-" + a.coll.toLowerCase() + "-labels " + (y || "")).add(v)); if (w || a.isLinked) c(m, function (b) { q[b] ? q[b].addLabel() : q[b] = new N(a, b) }), a.renderUnsquish(), !1 === F.reserveSpace || 0 !== r && 2 !== r && { 1: "left", 3: "right" }[r] !== a.labelAlign && "center" !== a.labelAlign || c(m, function (a) {
                        p =
                        Math.max(q[a].getLabelSize(), p)
                    }), a.staggerLines && (p *= a.staggerLines, a.labelOffset = p * (a.opposite ? -1 : 1)); else for (u in q) q[u].destroy(), delete q[u]; x && x.text && !1 !== x.enabled && (a.addTitle(f), f && (t = a.axisTitle.getBBox()[d ? "height" : "width"], z = x.offset, h = l(z) ? 0 : A(x.margin, d ? 5 : 10))); a.renderLine(); a.offset = J * A(e.offset, K[r]); a.tickRotCorr = a.tickRotCorr || { x: 0, y: 0 }; k = 0 === r ? -a.labelMetrics().h : 2 === r ? a.tickRotCorr.y : 0; h = Math.abs(p) + h; p && (h = h - k + J * (d ? A(F.y, a.tickRotCorr.y + 8 * J) : F.x)); a.axisTitleMargin = A(z, h);
                K[r] = Math.max(K[r], a.axisTitleMargin + t + J * a.offset, h, w && m.length && C ? C[0] : 0); e = e.offset ? 0 : 2 * Math.floor(a.axisLine.strokeWidth() / 2); b[n] = Math.max(b[n], e)
            }, getLinePath: function (a) { var b = this.chart, c = this.opposite, g = this.offset, k = this.horiz, e = this.left + (c ? this.width : 0) + g, g = b.chartHeight - this.bottom - (c ? this.height : 0) + g; c && (a *= -1); return b.renderer.crispLine(["M", k ? this.left : e, k ? g : this.top, "L", k ? b.chartWidth - this.right : e, k ? g : b.chartHeight - this.bottom], a) }, renderLine: function () {
            this.axisLine || (this.axisLine =
                this.chart.renderer.path().addClass("highcharts-axis-line").add(this.axisGroup), this.axisLine.attr({ stroke: this.options.lineColor, "stroke-width": this.options.lineWidth, zIndex: 7 }))
            }, getTitlePosition: function () {
                var a = this.horiz, b = this.left, c = this.top, k = this.len, e = this.options.title, m = a ? b : c, q = this.opposite, d = this.offset, r = e.x || 0, n = e.y || 0, w = this.chart.renderer.fontMetrics(e.style && e.style.fontSize, this.axisTitle).f, k = { low: m + (a ? 0 : k), middle: m + k / 2, high: m + (a ? k : 0) }[e.align], b = (a ? c + this.height : b) + (a ? 1 : -1) *
                    (q ? -1 : 1) * this.axisTitleMargin + (2 === this.side ? w : 0); return { x: a ? k + r : b + (q ? this.width : 0) + d + r, y: a ? b + n - (q ? this.height : 0) + d : k + n }
            }, render: function () {
                var a = this, b = a.chart, e = b.renderer, m = a.options, q = a.isLog, d = a.lin2log, r = a.isLinked, n = a.tickPositions, w = a.axisTitle, f = a.ticks, t = a.minorTicks, z = a.alternateBands, h = m.stackLabels, l = m.alternateGridColor, x = a.tickmarkOffset, p = a.axisLine, A = b.hasRendered && F(a.oldMin), K = a.showAxis, u = C(e.globalAnimation), y, v; a.labelEdge.length = 0; a.overlap = !1; c([f, t, z], function (a) {
                    for (var b in a) a[b].isActive =
                        !1
                }); if (a.hasData() || r) a.minorTickInterval && !a.categories && c(a.getMinorTickPositions(), function (b) { t[b] || (t[b] = new N(a, b, "minor")); A && t[b].isNew && t[b].render(null, !0); t[b].render(null, !1, 1) }), n.length && (c(n, function (b, c) { if (!r || b >= a.min && b <= a.max) f[b] || (f[b] = new N(a, b)), A && f[b].isNew && f[b].render(c, !0, .1), f[b].render(c) }), x && (0 === a.min || a.single) && (f[-1] || (f[-1] = new N(a, -1, null, !0)), f[-1].render(-1))), l && c(n, function (c, g) {
                    v = void 0 !== n[g + 1] ? n[g + 1] + x : a.max - x; 0 === g % 2 && c < a.max && v <= a.max + (b.polar ? -x : x) &&
                        (z[c] || (z[c] = new k(a)), y = c + x, z[c].options = { from: q ? d(y) : y, to: q ? d(v) : v, color: l }, z[c].render(), z[c].isActive = !0)
                }), a._addedPlotLB || (c((m.plotLines || []).concat(m.plotBands || []), function (b) { a.addPlotBandOrLine(b) }), a._addedPlotLB = !0); c([f, t, z], function (a) { var c, g, k = [], e = u.duration; for (c in a) a[c].isActive || (a[c].render(c, !1, 0), a[c].isActive = !1, k.push(c)); J(function () { for (g = k.length; g--;)a[k[g]] && !a[k[g]].isActive && (a[k[g]].destroy(), delete a[k[g]]) }, a !== z && b.hasRendered && e ? e : 0) }); p && (p[p.isPlaced ? "animate" :
                    "attr"]({ d: this.getLinePath(p.strokeWidth()) }), p.isPlaced = !0, p[K ? "show" : "hide"](!0)); w && K && (w[w.isNew ? "attr" : "animate"](a.getTitlePosition()), w.isNew = !1); h && h.enabled && a.renderStackTotals(); a.isDirty = !1
            }, redraw: function () { this.visible && (this.render(), c(this.plotLinesAndBands, function (a) { a.render() })); c(this.series, function (a) { a.isDirty = !0 }) }, keepProps: "extKey hcEvents names series userMax userMin".split(" "), destroy: function (a) {
                var b = this, g = b.stacks, k, e = b.plotLinesAndBands, m; a || w(b); for (k in g) d(g[k]),
                    g[k] = null; c([b.ticks, b.minorTicks, b.alternateBands], function (a) { d(a) }); if (e) for (a = e.length; a--;)e[a].destroy(); c("stackTotalGroup axisLine axisTitle axisGroup gridGroup labelGroup cross".split(" "), function (a) { b[a] && (b[a] = b[a].destroy()) }); for (m in b) b.hasOwnProperty(m) && -1 === q(m, b.keepProps) && delete b[m]
            }, drawCrosshair: function (a, b) {
                var c, g = this.crosshair, k = A(g.snap, !0), e, m = this.cross; a || (a = this.cross && this.cross.e); this.crosshair && !1 !== (l(b) || !k) ? (k ? l(b) && (e = this.isXAxis ? b.plotX : this.len - b.plotY) :
                    e = a && (this.horiz ? a.chartX - this.pos : this.len - a.chartY + this.pos), l(e) && (c = this.getPlotLinePath(b && (this.isXAxis ? b.x : A(b.stackY, b.y)), null, null, null, e) || null), l(c) ? (b = this.categories && !this.isRadial, m || (this.cross = m = this.chart.renderer.path().addClass("highcharts-crosshair highcharts-crosshair-" + (b ? "category " : "thin ") + g.className).attr({ zIndex: A(g.zIndex, 2) }).add(), m.attr({ stroke: g.color || (b ? f("#ccd6eb").setOpacity(.25).get() : "#cccccc"), "stroke-width": A(g.width, 1) }), g.dashStyle && m.attr({ dashstyle: g.dashStyle })),
                        m.show().attr({ d: c }), b && !g.width && m.attr({ "stroke-width": this.transA }), this.cross.e = a) : this.hideCrosshair()) : this.hideCrosshair()
            }, hideCrosshair: function () { this.cross && this.cross.hide() }
        }; n(a.Axis.prototype, h)
    })(L); (function (a) {
        var D = a.Axis, C = a.Date, G = a.dateFormat, I = a.defaultOptions, h = a.defined, f = a.each, p = a.extend, v = a.getMagnitude, l = a.getTZOffset, u = a.normalizeTickInterval, d = a.pick, c = a.timeUnits; D.prototype.getTimeTicks = function (a, y, t, m) {
            var b = [], q = {}, n = I.global.useUTC, F, e = new C(y - l(y)), r = C.hcMakeTime,
            x = a.unitRange, A = a.count, k; if (h(y)) {
                e[C.hcSetMilliseconds](x >= c.second ? 0 : A * Math.floor(e.getMilliseconds() / A)); if (x >= c.second) e[C.hcSetSeconds](x >= c.minute ? 0 : A * Math.floor(e.getSeconds() / A)); if (x >= c.minute) e[C.hcSetMinutes](x >= c.hour ? 0 : A * Math.floor(e[C.hcGetMinutes]() / A)); if (x >= c.hour) e[C.hcSetHours](x >= c.day ? 0 : A * Math.floor(e[C.hcGetHours]() / A)); if (x >= c.day) e[C.hcSetDate](x >= c.month ? 1 : A * Math.floor(e[C.hcGetDate]() / A)); x >= c.month && (e[C.hcSetMonth](x >= c.year ? 0 : A * Math.floor(e[C.hcGetMonth]() / A)), F = e[C.hcGetFullYear]());
                if (x >= c.year) e[C.hcSetFullYear](F - F % A); if (x === c.week) e[C.hcSetDate](e[C.hcGetDate]() - e[C.hcGetDay]() + d(m, 1)); F = e[C.hcGetFullYear](); m = e[C.hcGetMonth](); var w = e[C.hcGetDate](), K = e[C.hcGetHours](); if (C.hcTimezoneOffset || C.hcGetTimezoneOffset) k = (!n || !!C.hcGetTimezoneOffset) && (t - y > 4 * c.month || l(y) !== l(t)), e = e.getTime(), e = new C(e + l(e)); n = e.getTime(); for (y = 1; n < t;)b.push(n), n = x === c.year ? r(F + y * A, 0) : x === c.month ? r(F, m + y * A) : !k || x !== c.day && x !== c.week ? k && x === c.hour ? r(F, m, w, K + y * A) : n + x * A : r(F, m, w + y * A * (x === c.day ? 1 :
                    7)), y++; b.push(n); x <= c.hour && f(b, function (a) { "000000000" === G("%H%M%S%L", a) && (q[a] = "day") })
            } b.info = p(a, { higherRanks: q, totalRange: x * A }); return b
        }; D.prototype.normalizeTimeTickInterval = function (a, d) {
            var f = d || [["millisecond", [1, 2, 5, 10, 20, 25, 50, 100, 200, 500]], ["second", [1, 2, 5, 10, 15, 30]], ["minute", [1, 2, 5, 10, 15, 30]], ["hour", [1, 2, 3, 4, 6, 8, 12]], ["day", [1, 2]], ["week", [1, 2]], ["month", [1, 2, 3, 4, 6]], ["year", null]]; d = f[f.length - 1]; var m = c[d[0]], b = d[1], q; for (q = 0; q < f.length && !(d = f[q], m = c[d[0]], b = d[1], f[q + 1] && a <= (m *
                b[b.length - 1] + c[f[q + 1][0]]) / 2); q++); m === c.year && a < 5 * m && (b = [1, 2, 5]); a = u(a / m, b, "year" === d[0] ? Math.max(v(a / m), 1) : 1); return { unitRange: m, count: a, unitName: d[0] }
        }
    })(L); (function (a) {
        var D = a.Axis, C = a.getMagnitude, G = a.map, I = a.normalizeTickInterval, h = a.pick; D.prototype.getLogTickPositions = function (a, p, v, l) {
            var f = this.options, d = this.len, c = this.lin2log, n = this.log2lin, y = []; l || (this._minorAutoInterval = null); if (.5 <= a) a = Math.round(a), y = this.getLinearTickPositions(a, p, v); else if (.08 <= a) for (var d = Math.floor(p), t, m,
                b, q, z, f = .3 < a ? [1, 2, 4] : .15 < a ? [1, 2, 4, 6, 8] : [1, 2, 3, 4, 5, 6, 7, 8, 9]; d < v + 1 && !z; d++)for (m = f.length, t = 0; t < m && !z; t++)b = n(c(d) * f[t]), b > p && (!l || q <= v) && void 0 !== q && y.push(q), q > v && (z = !0), q = b; else p = c(p), v = c(v), a = f[l ? "minorTickInterval" : "tickInterval"], a = h("auto" === a ? null : a, this._minorAutoInterval, f.tickPixelInterval / (l ? 5 : 1) * (v - p) / ((l ? d / this.tickPositions.length : d) || 1)), a = I(a, null, C(a)), y = G(this.getLinearTickPositions(a, p, v), n), l || (this._minorAutoInterval = a / 5); l || (this.tickInterval = a); return y
        }; D.prototype.log2lin =
            function (a) { return Math.log(a) / Math.LN10 }; D.prototype.lin2log = function (a) { return Math.pow(10, a) }
    })(L); (function (a) {
        var D = a.dateFormat, C = a.each, G = a.extend, I = a.format, h = a.isNumber, f = a.map, p = a.merge, v = a.pick, l = a.splat, u = a.syncTimeout, d = a.timeUnits; a.Tooltip = function () { this.init.apply(this, arguments) }; a.Tooltip.prototype = {
            init: function (a, d) { this.chart = a; this.options = d; this.crosshairs = []; this.now = { x: 0, y: 0 }; this.isHidden = !0; this.split = d.split && !a.inverted; this.shared = d.shared || this.split }, cleanSplit: function (a) {
                C(this.chart.series,
                    function (c) { var d = c && c.tt; d && (!d.isActive || a ? c.tt = d.destroy() : d.isActive = !1) })
            }, getLabel: function () { var a = this.chart.renderer, d = this.options; this.label || (this.split ? this.label = a.g("tooltip") : (this.label = a.label("", 0, 0, d.shape || "callout", null, null, d.useHTML, null, "tooltip").attr({ padding: d.padding, r: d.borderRadius }), this.label.attr({ fill: d.backgroundColor, "stroke-width": d.borderWidth }).css(d.style).shadow(d.shadow)), this.label.attr({ zIndex: 8 }).add()); return this.label }, update: function (a) {
                this.destroy();
                this.init(this.chart, p(!0, this.options, a))
            }, destroy: function () { this.label && (this.label = this.label.destroy()); this.split && this.tt && (this.cleanSplit(this.chart, !0), this.tt = this.tt.destroy()); clearTimeout(this.hideTimer); clearTimeout(this.tooltipTimeout) }, move: function (a, d, f, t) {
                var c = this, b = c.now, q = !1 !== c.options.animation && !c.isHidden && (1 < Math.abs(a - b.x) || 1 < Math.abs(d - b.y)), n = c.followPointer || 1 < c.len; G(b, {
                    x: q ? (2 * b.x + a) / 3 : a, y: q ? (b.y + d) / 2 : d, anchorX: n ? void 0 : q ? (2 * b.anchorX + f) / 3 : f, anchorY: n ? void 0 : q ? (b.anchorY +
                        t) / 2 : t
                }); c.getLabel().attr(b); q && (clearTimeout(this.tooltipTimeout), this.tooltipTimeout = setTimeout(function () { c && c.move(a, d, f, t) }, 32))
            }, hide: function (a) { var c = this; clearTimeout(this.hideTimer); a = v(a, this.options.hideDelay, 500); this.isHidden || (this.hideTimer = u(function () { c.getLabel()[a ? "fadeOut" : "hide"](); c.isHidden = !0 }, a)) }, getAnchor: function (a, d) {
                var c, n = this.chart, m = n.inverted, b = n.plotTop, q = n.plotLeft, z = 0, h = 0, e, r; a = l(a); c = a[0].tooltipPos; this.followPointer && d && (void 0 === d.chartX && (d = n.pointer.normalize(d)),
                    c = [d.chartX - n.plotLeft, d.chartY - b]); c || (C(a, function (a) { e = a.series.yAxis; r = a.series.xAxis; z += a.plotX + (!m && r ? r.left - q : 0); h += (a.plotLow ? (a.plotLow + a.plotHigh) / 2 : a.plotY) + (!m && e ? e.top - b : 0) }), z /= a.length, h /= a.length, c = [m ? n.plotWidth - h : z, this.shared && !m && 1 < a.length && d ? d.chartY - b : m ? n.plotHeight - z : h]); return f(c, Math.round)
            }, getPosition: function (a, d, f) {
                var c = this.chart, m = this.distance, b = {}, q = f.h || 0, n, h = ["y", c.chartHeight, d, f.plotY + c.plotTop, c.plotTop, c.plotTop + c.plotHeight], e = ["x", c.chartWidth, a, f.plotX +
                    c.plotLeft, c.plotLeft, c.plotLeft + c.plotWidth], r = !this.followPointer && v(f.ttBelow, !c.inverted === !!f.negative), l = function (a, c, k, g, e, d) { var f = k < g - m, w = g + m + k < c, n = g - m - k; g += m; if (r && w) b[a] = g; else if (!r && f) b[a] = n; else if (f) b[a] = Math.min(d - k, 0 > n - q ? n : n - q); else if (w) b[a] = Math.max(e, g + q + k > c ? g : g + q); else return !1 }, p = function (a, c, k, g) { var e; g < m || g > c - m ? e = !1 : b[a] = g < k / 2 ? 1 : g > c - k / 2 ? c - k - 2 : g - k / 2; return e }, k = function (a) { var b = h; h = e; e = b; n = a }, w = function () {
                    !1 !== l.apply(0, h) ? !1 !== p.apply(0, e) || n || (k(!0), w()) : n ? b.x = b.y = 0 : (k(!0),
                        w())
                    }; (c.inverted || 1 < this.len) && k(); w(); return b
            }, defaultFormatter: function (a) { var c = this.points || l(this), d; d = [a.tooltipFooterHeaderFormatter(c[0])]; d = d.concat(a.bodyFormatter(c)); d.push(a.tooltipFooterHeaderFormatter(c[0], !0)); return d }, refresh: function (a, d) {
                var c = this.chart, f, m = this.options, b, q, n = {}, h = []; f = m.formatter || this.defaultFormatter; var n = c.hoverPoints, e = this.shared; clearTimeout(this.hideTimer); this.followPointer = l(a)[0].series.tooltipOptions.followPointer; q = this.getAnchor(a, d); d = q[0]; b =
                    q[1]; !e || a.series && a.series.noSharedTooltip ? n = a.getLabelConfig() : (c.hoverPoints = a, n && C(n, function (a) { a.setState() }), C(a, function (a) { a.setState("hover"); h.push(a.getLabelConfig()) }), n = { x: a[0].category, y: a[0].y }, n.points = h, this.len = h.length, a = a[0]); n = f.call(n, this); e = a.series; this.distance = v(e.tooltipOptions.distance, 16); !1 === n ? this.hide() : (f = this.getLabel(), this.isHidden && f.attr({ opacity: 1 }).show(), this.split ? this.renderSplit(n, c.hoverPoints) : (f.attr({ text: n && n.join ? n.join("") : n }), f.removeClass(/highcharts-color-[\d]+/g).addClass("highcharts-color-" +
                        v(a.colorIndex, e.colorIndex)), f.attr({ stroke: m.borderColor || a.color || e.color || "#666666" }), this.updatePosition({ plotX: d, plotY: b, negative: a.negative, ttBelow: a.ttBelow, h: q[2] || 0 })), this.isHidden = !1)
            }, renderSplit: function (c, d) {
                var f = this, n = [], m = this.chart, b = m.renderer, q = !0, h = this.options, l, e = this.getLabel(); C(c.slice(0, c.length - 1), function (a, c) {
                    c = d[c - 1] || { isHeader: !0, plotX: d[0].plotX }; var r = c.series || f, k = r.tt, w = c.series || {}, t = "highcharts-color-" + v(c.colorIndex, w.colorIndex, "none"); k || (r.tt = k = b.label(null,
                        null, null, "callout").addClass("highcharts-tooltip-box " + t).attr({ padding: h.padding, r: h.borderRadius, fill: h.backgroundColor, stroke: c.color || w.color || "#333333", "stroke-width": h.borderWidth }).add(e)); k.isActive = !0; k.attr({ text: a }); k.css(h.style); a = k.getBBox(); w = a.width + k.strokeWidth(); c.isHeader ? (l = a.height, w = Math.max(0, Math.min(c.plotX + m.plotLeft - w / 2, m.chartWidth - w))) : w = c.plotX + m.plotLeft - v(h.distance, 16) - w; 0 > w && (q = !1); a = (c.series && c.series.yAxis && c.series.yAxis.pos) + (c.plotY || 0); a -= m.plotTop; n.push({
                            target: c.isHeader ?
                                m.plotHeight + l : a, rank: c.isHeader ? 1 : 0, size: r.tt.getBBox().height + 1, point: c, x: w, tt: k
                        })
                }); this.cleanSplit(); a.distribute(n, m.plotHeight + l); C(n, function (a) { var b = a.point, c = b.series; a.tt.attr({ visibility: void 0 === a.pos ? "hidden" : "inherit", x: q || b.isHeader ? a.x : b.plotX + m.plotLeft + v(h.distance, 16), y: a.pos + m.plotTop, anchorX: b.isHeader ? b.plotX + m.plotLeft : b.plotX + c.xAxis.pos, anchorY: b.isHeader ? a.pos + m.plotTop - 15 : b.plotY + c.yAxis.pos }) })
            }, updatePosition: function (a) {
                var c = this.chart, d = this.getLabel(), d = (this.options.positioner ||
                    this.getPosition).call(this, d.width, d.height, a); this.move(Math.round(d.x), Math.round(d.y || 0), a.plotX + c.plotLeft, a.plotY + c.plotTop)
            }, getXDateFormat: function (a, f, h) {
                var c; f = f.dateTimeLabelFormats; var m = h && h.closestPointRange, b, q = { millisecond: 15, second: 12, minute: 9, hour: 6, day: 3 }, n, l = "millisecond"; if (m) {
                    n = D("%m-%d %H:%M:%S.%L", a.x); for (b in d) {
                        if (m === d.week && +D("%w", a.x) === h.options.startOfWeek && "00:00:00.000" === n.substr(6)) { b = "week"; break } if (d[b] > m) { b = l; break } if (q[b] && n.substr(q[b]) !== "01-01 00:00:00.000".substr(q[b])) break;
                        "week" !== b && (l = b)
                    } b && (c = f[b])
                } else c = f.day; return c || f.year
            }, tooltipFooterHeaderFormatter: function (a, d) { var c = d ? "footer" : "header"; d = a.series; var f = d.tooltipOptions, m = f.xDateFormat, b = d.xAxis, q = b && "datetime" === b.options.type && h(a.key), c = f[c + "Format"]; q && !m && (m = this.getXDateFormat(a, f, b)); q && m && (c = c.replace("{point.key}", "{point.key:" + m + "}")); return I(c, { point: a, series: d }) }, bodyFormatter: function (a) {
                return f(a, function (a) {
                    var c = a.series.tooltipOptions; return (c.pointFormatter || a.point.tooltipFormatter).call(a.point,
                        c.pointFormat)
                })
            }
        }
    })(L); (function (a) {
        var D = a.addEvent, C = a.attr, G = a.charts, I = a.color, h = a.css, f = a.defined, p = a.doc, v = a.each, l = a.extend, u = a.fireEvent, d = a.offset, c = a.pick, n = a.removeEvent, y = a.splat, t = a.Tooltip, m = a.win; a.Pointer = function (a, c) { this.init(a, c) }; a.Pointer.prototype = {
            init: function (a, m) {
            this.options = m; this.chart = a; this.runChartClick = m.chart.events && !!m.chart.events.click; this.pinchDown = []; this.lastValidTouch = {}; t && m.tooltip.enabled && (a.tooltip = new t(a, m.tooltip), this.followTouchMove = c(m.tooltip.followTouchMove,
                !0)); this.setDOMEvents()
            }, zoomOption: function (a) { var b = this.chart, m = b.options.chart, d = m.zoomType || "", b = b.inverted; /touch/.test(a.type) && (d = c(m.pinchType, d)); this.zoomX = a = /x/.test(d); this.zoomY = d = /y/.test(d); this.zoomHor = a && !b || d && b; this.zoomVert = d && !b || a && b; this.hasZoom = a || d }, normalize: function (a, c) {
                var b, q; a = a || m.event; a.target || (a.target = a.srcElement); q = a.touches ? a.touches.length ? a.touches.item(0) : a.changedTouches[0] : a; c || (this.chartPosition = c = d(this.chart.container)); void 0 === q.pageX ? (b = Math.max(a.x,
                    a.clientX - c.left), c = a.y) : (b = q.pageX - c.left, c = q.pageY - c.top); return l(a, { chartX: Math.round(b), chartY: Math.round(c) })
            }, getCoordinates: function (a) { var b = { xAxis: [], yAxis: [] }; v(this.chart.axes, function (c) { b[c.isXAxis ? "xAxis" : "yAxis"].push({ axis: c, value: c.toValue(a[c.horiz ? "chartX" : "chartY"]) }) }); return b }, runPointActions: function (b) {
                var m = this.chart, d = m.series, f = m.tooltip, e = f ? f.shared : !1, r = !0, n = m.hoverPoint, h = m.hoverSeries, k, w, l, t = [], u; if (!e && !h) for (k = 0; k < d.length; k++)if (d[k].directTouch || !d[k].options.stickyTracking) d =
                    []; h && (e ? h.noSharedTooltip : h.directTouch) && n ? t = [n] : (e || !h || h.options.stickyTracking || (d = [h]), v(d, function (a) { w = a.noSharedTooltip && e; l = !e && a.directTouch; a.visible && !w && !l && c(a.options.enableMouseTracking, !0) && (u = a.searchPoint(b, !w && 1 === a.kdDimensions)) && u.series && t.push(u) }), t.sort(function (a, b) { var c = a.distX - b.distX, g = a.dist - b.dist, k = b.series.group.zIndex - a.series.group.zIndex; return 0 !== c && e ? c : 0 !== g ? g : 0 !== k ? k : a.series.index > b.series.index ? -1 : 1 })); if (e) for (k = t.length; k--;)(t[k].x !== t[0].x || t[k].series.noSharedTooltip) &&
                        t.splice(k, 1); if (t[0] && (t[0] !== this.prevKDPoint || f && f.isHidden)) { if (e && !t[0].series.noSharedTooltip) { for (k = 0; k < t.length; k++)t[k].onMouseOver(b, t[k] !== (h && h.directTouch && n || t[0])); t.length && f && f.refresh(t.sort(function (a, b) { return a.series.index - b.series.index }), b) } else if (f && f.refresh(t[0], b), !h || !h.directTouch) t[0].onMouseOver(b); this.prevKDPoint = t[0]; r = !1 } r && (d = h && h.tooltipOptions.followPointer, f && d && !f.isHidden && (d = f.getAnchor([{}], b), f.updatePosition({ plotX: d[0], plotY: d[1] }))); this.unDocMouseMove ||
                            (this.unDocMouseMove = D(p, "mousemove", function (b) { if (G[a.hoverChartIndex]) G[a.hoverChartIndex].pointer.onDocumentMouseMove(b) })); v(e ? t : [c(n, t[0])], function (a) { v(m.axes, function (c) { (!a || a.series && a.series[c.coll] === c) && c.drawCrosshair(b, a) }) })
            }, reset: function (a, c) {
                var b = this.chart, m = b.hoverSeries, e = b.hoverPoint, d = b.hoverPoints, q = b.tooltip, f = q && q.shared ? d : e; a && f && v(y(f), function (b) { b.series.isCartesian && void 0 === b.plotX && (a = !1) }); if (a) q && f && (q.refresh(f), e && (e.setState(e.state, !0), v(b.axes, function (a) {
                a.crosshair &&
                    a.drawCrosshair(null, e)
                }))); else { if (e) e.onMouseOut(); d && v(d, function (a) { a.setState() }); if (m) m.onMouseOut(); q && q.hide(c); this.unDocMouseMove && (this.unDocMouseMove = this.unDocMouseMove()); v(b.axes, function (a) { a.hideCrosshair() }); this.hoverX = this.prevKDPoint = b.hoverPoints = b.hoverPoint = null }
            }, scaleGroups: function (a, c) {
                var b = this.chart, m; v(b.series, function (e) {
                    m = a || e.getPlotBox(); e.xAxis && e.xAxis.zoomEnabled && e.group && (e.group.attr(m), e.markerGroup && (e.markerGroup.attr(m), e.markerGroup.clip(c ? b.clipRect :
                        null)), e.dataLabelsGroup && e.dataLabelsGroup.attr(m))
                }); b.clipRect.attr(c || b.clipBox)
            }, dragStart: function (a) { var b = this.chart; b.mouseIsDown = a.type; b.cancelClick = !1; b.mouseDownX = this.mouseDownX = a.chartX; b.mouseDownY = this.mouseDownY = a.chartY }, drag: function (a) {
                var b = this.chart, c = b.options.chart, m = a.chartX, e = a.chartY, d = this.zoomHor, f = this.zoomVert, n = b.plotLeft, k = b.plotTop, w = b.plotWidth, h = b.plotHeight, l, t = this.selectionMarker, g = this.mouseDownX, p = this.mouseDownY, u = c.panKey && a[c.panKey + "Key"]; t && t.touch ||
                    (m < n ? m = n : m > n + w && (m = n + w), e < k ? e = k : e > k + h && (e = k + h), this.hasDragged = Math.sqrt(Math.pow(g - m, 2) + Math.pow(p - e, 2)), 10 < this.hasDragged && (l = b.isInsidePlot(g - n, p - k), b.hasCartesianSeries && (this.zoomX || this.zoomY) && l && !u && !t && (this.selectionMarker = t = b.renderer.rect(n, k, d ? 1 : w, f ? 1 : h, 0).attr({ fill: c.selectionMarkerFill || I("#335cad").setOpacity(.25).get(), "class": "highcharts-selection-marker", zIndex: 7 }).add()), t && d && (m -= g, t.attr({ width: Math.abs(m), x: (0 < m ? 0 : m) + g })), t && f && (m = e - p, t.attr({
                        height: Math.abs(m), y: (0 < m ? 0 : m) +
                            p
                    })), l && !t && c.panning && b.pan(a, c.panning)))
            }, drop: function (a) {
                var b = this, c = this.chart, m = this.hasPinched; if (this.selectionMarker) {
                    var e = { originalEvent: a, xAxis: [], yAxis: [] }, d = this.selectionMarker, n = d.attr ? d.attr("x") : d.x, t = d.attr ? d.attr("y") : d.y, k = d.attr ? d.attr("width") : d.width, w = d.attr ? d.attr("height") : d.height, p; if (this.hasDragged || m) v(c.axes, function (c) {
                        if (c.zoomEnabled && f(c.min) && (m || b[{ xAxis: "zoomX", yAxis: "zoomY" }[c.coll]])) {
                            var d = c.horiz, g = "touchend" === a.type ? c.minPixelPadding : 0, q = c.toValue((d ?
                                n : t) + g), d = c.toValue((d ? n + k : t + w) - g); e[c.coll].push({ axis: c, min: Math.min(q, d), max: Math.max(q, d) }); p = !0
                        }
                    }), p && u(c, "selection", e, function (a) { c.zoom(l(a, m ? { animation: !1 } : null)) }); this.selectionMarker = this.selectionMarker.destroy(); m && this.scaleGroups()
                } c && (h(c.container, { cursor: c._cursor }), c.cancelClick = 10 < this.hasDragged, c.mouseIsDown = this.hasDragged = this.hasPinched = !1, this.pinchDown = [])
            }, onContainerMouseDown: function (a) { a = this.normalize(a); this.zoomOption(a); a.preventDefault && a.preventDefault(); this.dragStart(a) },
            onDocumentMouseUp: function (b) { G[a.hoverChartIndex] && G[a.hoverChartIndex].pointer.drop(b) }, onDocumentMouseMove: function (a) { var b = this.chart, c = this.chartPosition; a = this.normalize(a, c); !c || this.inClass(a.target, "highcharts-tracker") || b.isInsidePlot(a.chartX - b.plotLeft, a.chartY - b.plotTop) || this.reset() }, onContainerMouseLeave: function (b) { var c = G[a.hoverChartIndex]; c && (b.relatedTarget || b.toElement) && (c.pointer.reset(), c.pointer.chartPosition = null) }, onContainerMouseMove: function (b) {
                var c = this.chart; f(a.hoverChartIndex) &&
                    G[a.hoverChartIndex] && G[a.hoverChartIndex].mouseIsDown || (a.hoverChartIndex = c.index); b = this.normalize(b); b.returnValue = !1; "mousedown" === c.mouseIsDown && this.drag(b); !this.inClass(b.target, "highcharts-tracker") && !c.isInsidePlot(b.chartX - c.plotLeft, b.chartY - c.plotTop) || c.openMenu || this.runPointActions(b)
            }, inClass: function (a, c) { for (var b; a;) { if (b = C(a, "class")) { if (-1 !== b.indexOf(c)) return !0; if (-1 !== b.indexOf("highcharts-container")) return !1 } a = a.parentNode } }, onTrackerMouseOut: function (a) {
                var b = this.chart.hoverSeries;
                a = a.relatedTarget || a.toElement; if (!(!b || !a || b.options.stickyTracking || this.inClass(a, "highcharts-tooltip") || this.inClass(a, "highcharts-series-" + b.index) && this.inClass(a, "highcharts-tracker"))) b.onMouseOut()
            }, onContainerClick: function (a) {
                var b = this.chart, c = b.hoverPoint, m = b.plotLeft, e = b.plotTop; a = this.normalize(a); b.cancelClick || (c && this.inClass(a.target, "highcharts-tracker") ? (u(c.series, "click", l(a, { point: c })), b.hoverPoint && c.firePointEvent("click", a)) : (l(a, this.getCoordinates(a)), b.isInsidePlot(a.chartX -
                    m, a.chartY - e) && u(b, "click", a)))
            }, setDOMEvents: function () { var b = this, c = b.chart.container; c.onmousedown = function (a) { b.onContainerMouseDown(a) }; c.onmousemove = function (a) { b.onContainerMouseMove(a) }; c.onclick = function (a) { b.onContainerClick(a) }; D(c, "mouseleave", b.onContainerMouseLeave); 1 === a.chartCount && D(p, "mouseup", b.onDocumentMouseUp); a.hasTouch && (c.ontouchstart = function (a) { b.onContainerTouchStart(a) }, c.ontouchmove = function (a) { b.onContainerTouchMove(a) }, 1 === a.chartCount && D(p, "touchend", b.onDocumentTouchEnd)) },
            destroy: function () { var b; n(this.chart.container, "mouseleave", this.onContainerMouseLeave); a.chartCount || (n(p, "mouseup", this.onDocumentMouseUp), n(p, "touchend", this.onDocumentTouchEnd)); clearInterval(this.tooltipTimeout); for (b in this) this[b] = null }
        }
    })(L); (function (a) {
        var D = a.charts, C = a.each, G = a.extend, I = a.map, h = a.noop, f = a.pick; G(a.Pointer.prototype, {
            pinchTranslate: function (a, f, h, u, d, c) {
            this.zoomHor && this.pinchTranslateDirection(!0, a, f, h, u, d, c); this.zoomVert && this.pinchTranslateDirection(!1, a, f, h, u, d,
                c)
            }, pinchTranslateDirection: function (a, f, h, u, d, c, n, y) {
                var t = this.chart, m = a ? "x" : "y", b = a ? "X" : "Y", q = "chart" + b, l = a ? "width" : "height", p = t["plot" + (a ? "Left" : "Top")], e, r, x = y || 1, A = t.inverted, k = t.bounds[a ? "h" : "v"], w = 1 === f.length, K = f[0][q], J = h[0][q], v = !w && f[1][q], g = !w && h[1][q], B; h = function () { !w && 20 < Math.abs(K - v) && (x = y || Math.abs(J - g) / Math.abs(K - v)); r = (p - J) / x + K; e = t["plot" + (a ? "Width" : "Height")] / x }; h(); f = r; f < k.min ? (f = k.min, B = !0) : f + e > k.max && (f = k.max - e, B = !0); B ? (J -= .8 * (J - n[m][0]), w || (g -= .8 * (g - n[m][1])), h()) : n[m] = [J,
                    g]; A || (c[m] = r - p, c[l] = e); c = A ? 1 / x : x; d[l] = e; d[m] = f; u[A ? a ? "scaleY" : "scaleX" : "scale" + b] = x; u["translate" + b] = c * p + (J - c * K)
            }, pinch: function (a) {
                var p = this, l = p.chart, u = p.pinchDown, d = a.touches, c = d.length, n = p.lastValidTouch, y = p.hasZoom, t = p.selectionMarker, m = {}, b = 1 === c && (p.inClass(a.target, "highcharts-tracker") && l.runTrackerClick || p.runChartClick), q = {}; 1 < c && (p.initiated = !0); y && p.initiated && !b && a.preventDefault(); I(d, function (a) { return p.normalize(a) }); "touchstart" === a.type ? (C(d, function (a, b) {
                u[b] = {
                    chartX: a.chartX,
                    chartY: a.chartY
                }
                }), n.x = [u[0].chartX, u[1] && u[1].chartX], n.y = [u[0].chartY, u[1] && u[1].chartY], C(l.axes, function (a) { if (a.zoomEnabled) { var b = l.bounds[a.horiz ? "h" : "v"], c = a.minPixelPadding, m = a.toPixels(f(a.options.min, a.dataMin)), d = a.toPixels(f(a.options.max, a.dataMax)), q = Math.max(m, d); b.min = Math.min(a.pos, Math.min(m, d) - c); b.max = Math.max(a.pos + a.len, q + c) } }), p.res = !0) : p.followTouchMove && 1 === c ? this.runPointActions(p.normalize(a)) : u.length && (t || (p.selectionMarker = t = G({ destroy: h, touch: !0 }, l.plotBox)), p.pinchTranslate(u,
                    d, m, t, q, n), p.hasPinched = y, p.scaleGroups(m, q), p.res && (p.res = !1, this.reset(!1, 0)))
            }, touch: function (h, v) {
                var l = this.chart, p, d; if (l.index !== a.hoverChartIndex) this.onContainerMouseLeave({ relatedTarget: !0 }); a.hoverChartIndex = l.index; 1 === h.touches.length ? (h = this.normalize(h), (d = l.isInsidePlot(h.chartX - l.plotLeft, h.chartY - l.plotTop)) && !l.openMenu ? (v && this.runPointActions(h), "touchmove" === h.type && (v = this.pinchDown, p = v[0] ? 4 <= Math.sqrt(Math.pow(v[0].chartX - h.chartX, 2) + Math.pow(v[0].chartY - h.chartY, 2)) : !1), f(p,
                    !0) && this.pinch(h)) : v && this.reset()) : 2 === h.touches.length && this.pinch(h)
            }, onContainerTouchStart: function (a) { this.zoomOption(a); this.touch(a, !0) }, onContainerTouchMove: function (a) { this.touch(a) }, onDocumentTouchEnd: function (f) { D[a.hoverChartIndex] && D[a.hoverChartIndex].pointer.drop(f) }
        })
    })(L); (function (a) {
        var D = a.addEvent, C = a.charts, G = a.css, I = a.doc, h = a.extend, f = a.noop, p = a.Pointer, v = a.removeEvent, l = a.win, u = a.wrap; if (l.PointerEvent || l.MSPointerEvent) {
            var d = {}, c = !!l.PointerEvent, n = function () {
                var a, c = [];
                c.item = function (a) { return this[a] }; for (a in d) d.hasOwnProperty(a) && c.push({ pageX: d[a].pageX, pageY: d[a].pageY, target: d[a].target }); return c
            }, y = function (c, m, b, d) { "touch" !== c.pointerType && c.pointerType !== c.MSPOINTER_TYPE_TOUCH || !C[a.hoverChartIndex] || (d(c), d = C[a.hoverChartIndex].pointer, d[m]({ type: b, target: c.currentTarget, preventDefault: f, touches: n() })) }; h(p.prototype, {
                onContainerPointerDown: function (a) {
                    y(a, "onContainerTouchStart", "touchstart", function (a) {
                    d[a.pointerId] = {
                        pageX: a.pageX, pageY: a.pageY,
                        target: a.currentTarget
                    }
                    })
                }, onContainerPointerMove: function (a) { y(a, "onContainerTouchMove", "touchmove", function (a) { d[a.pointerId] = { pageX: a.pageX, pageY: a.pageY }; d[a.pointerId].target || (d[a.pointerId].target = a.currentTarget) }) }, onDocumentPointerUp: function (a) { y(a, "onDocumentTouchEnd", "touchend", function (a) { delete d[a.pointerId] }) }, batchMSEvents: function (a) {
                    a(this.chart.container, c ? "pointerdown" : "MSPointerDown", this.onContainerPointerDown); a(this.chart.container, c ? "pointermove" : "MSPointerMove", this.onContainerPointerMove);
                    a(I, c ? "pointerup" : "MSPointerUp", this.onDocumentPointerUp)
                }
            }); u(p.prototype, "init", function (a, c, b) { a.call(this, c, b); this.hasZoom && G(c.container, { "-ms-touch-action": "none", "touch-action": "none" }) }); u(p.prototype, "setDOMEvents", function (a) { a.apply(this); (this.hasZoom || this.followTouchMove) && this.batchMSEvents(D) }); u(p.prototype, "destroy", function (a) { this.batchMSEvents(v); a.call(this) })
        }
    })(L); (function (a) {
        var D, C = a.addEvent, G = a.css, I = a.discardElement, h = a.defined, f = a.each, p = a.extend, v = a.isFirefox, l = a.marginNames,
        u = a.merge, d = a.pick, c = a.setAnimation, n = a.stableSort, y = a.win, t = a.wrap; D = a.Legend = function (a, b) { this.init(a, b) }; D.prototype = {
            init: function (a, b) { this.chart = a; this.setOptions(b); b.enabled && (this.render(), C(this.chart, "endResize", function () { this.legend.positionCheckboxes() })) }, setOptions: function (a) {
                var b = d(a.padding, 8); this.options = a; this.itemStyle = a.itemStyle; this.itemHiddenStyle = u(this.itemStyle, a.itemHiddenStyle); this.itemMarginTop = a.itemMarginTop || 0; this.initialItemX = this.padding = b; this.initialItemY =
                    b - 5; this.itemHeight = this.maxItemWidth = 0; this.symbolWidth = d(a.symbolWidth, 16); this.pages = []
            }, update: function (a, b) { var c = this.chart; this.setOptions(u(!0, this.options, a)); this.destroy(); c.isDirtyLegend = c.isDirtyBox = !0; d(b, !0) && c.redraw() }, colorizeItem: function (a, b) {
            a.legendGroup[b ? "removeClass" : "addClass"]("highcharts-legend-item-hidden"); var c = this.options, d = a.legendItem, m = a.legendLine, e = a.legendSymbol, f = this.itemHiddenStyle.color, c = b ? c.itemStyle.color : f, h = b ? a.color || f : f, n = a.options && a.options.marker,
                k = { fill: h }, w; d && d.css({ fill: c, color: c }); m && m.attr({ stroke: h }); if (e) { if (n && e.isMarker && (k = a.pointAttribs(), !b)) for (w in k) k[w] = f; e.attr(k) }
            }, positionItem: function (a) { var b = this.options, c = b.symbolPadding, b = !b.rtl, d = a._legendItemPos, m = d[0], d = d[1], e = a.checkbox; (a = a.legendGroup) && a.element && a.translate(b ? m : this.legendWidth - m - 2 * c - 4, d); e && (e.x = m, e.y = d) }, destroyItem: function (a) { var b = a.checkbox; f(["legendItem", "legendLine", "legendSymbol", "legendGroup"], function (b) { a[b] && (a[b] = a[b].destroy()) }); b && I(a.checkbox) },
            destroy: function () { function a(a) { this[a] && (this[a] = this[a].destroy()) } f(this.getAllItems(), function (b) { f(["legendItem", "legendGroup"], a, b) }); f(["box", "title", "group"], a, this); this.display = null }, positionCheckboxes: function (a) { var b = this.group && this.group.alignAttr, c, d = this.clipHeight || this.legendHeight, m = this.titleHeight; b && (c = b.translateY, f(this.allItems, function (e) { var f = e.checkbox, h; f && (h = c + m + f.y + (a || 0) + 3, G(f, { left: b.translateX + e.checkboxOffset + f.x - 20 + "px", top: h + "px", display: h > c - 6 && h < c + d - 6 ? "" : "none" })) })) },
            renderTitle: function () { var a = this.padding, b = this.options.title, c = 0; b.text && (this.title || (this.title = this.chart.renderer.label(b.text, a - 3, a - 4, null, null, null, null, null, "legend-title").attr({ zIndex: 1 }).css(b.style).add(this.group)), a = this.title.getBBox(), c = a.height, this.offsetWidth = a.width, this.contentGroup.attr({ translateY: c })); this.titleHeight = c }, setText: function (c) { var b = this.options; c.legendItem.attr({ text: b.labelFormat ? a.format(b.labelFormat, c) : b.labelFormatter.call(c) }) }, renderItem: function (a) {
                var b =
                    this.chart, c = b.renderer, m = this.options, f = "horizontal" === m.layout, e = this.symbolWidth, h = m.symbolPadding, n = this.itemStyle, l = this.itemHiddenStyle, k = this.padding, w = f ? d(m.itemDistance, 20) : 0, t = !m.rtl, p = m.width, y = m.itemMarginBottom || 0, g = this.itemMarginTop, B = this.initialItemX, v = a.legendItem, M = !a.series, C = !M && a.series.drawLegendSymbol ? a.series : a, E = C.options, E = this.createCheckboxForItem && E && E.showCheckbox, H = m.useHTML; v || (a.legendGroup = c.g("legend-item").addClass("highcharts-" + C.type + "-series highcharts-color-" +
                        a.colorIndex + (a.options.className ? " " + a.options.className : "") + (M ? " highcharts-series-" + a.index : "")).attr({ zIndex: 1 }).add(this.scrollGroup), a.legendItem = v = c.text("", t ? e + h : -h, this.baseline || 0, H).css(u(a.visible ? n : l)).attr({ align: t ? "left" : "right", zIndex: 2 }).add(a.legendGroup), this.baseline || (n = n.fontSize, this.fontMetrics = c.fontMetrics(n, v), this.baseline = this.fontMetrics.f + 3 + g, v.attr("y", this.baseline)), C.drawLegendSymbol(this, a), this.setItemEvents && this.setItemEvents(a, v, H), E && this.createCheckboxForItem(a));
                this.colorizeItem(a, a.visible); this.setText(a); c = v.getBBox(); e = a.checkboxOffset = m.itemWidth || a.legendItemWidth || e + h + c.width + w + (E ? 20 : 0); this.itemHeight = h = Math.round(a.legendItemHeight || c.height); f && this.itemX - B + e > (p || b.chartWidth - 2 * k - B - m.x) && (this.itemX = B, this.itemY += g + this.lastLineHeight + y, this.lastLineHeight = 0); this.maxItemWidth = Math.max(this.maxItemWidth, e); this.lastItemY = g + this.itemY + y; this.lastLineHeight = Math.max(h, this.lastLineHeight); a._legendItemPos = [this.itemX, this.itemY]; f ? this.itemX += e :
                    (this.itemY += g + h + y, this.lastLineHeight = h); this.offsetWidth = p || Math.max((f ? this.itemX - B - w : e) + k, this.offsetWidth)
            }, getAllItems: function () { var a = []; f(this.chart.series, function (b) { var c = b && b.options; b && d(c.showInLegend, h(c.linkedTo) ? !1 : void 0, !0) && (a = a.concat(b.legendItems || ("point" === c.legendType ? b.data : b))) }); return a }, adjustMargins: function (a, b) {
                var c = this.chart, m = this.options, n = m.align.charAt(0) + m.verticalAlign.charAt(0) + m.layout.charAt(0); m.floating || f([/(lth|ct|rth)/, /(rtv|rm|rbv)/, /(rbh|cb|lbh)/,
                    /(lbv|lm|ltv)/], function (e, f) { e.test(n) && !h(a[f]) && (c[l[f]] = Math.max(c[l[f]], c.legend[(f + 1) % 2 ? "legendHeight" : "legendWidth"] + [1, -1, -1, 1][f] * m[f % 2 ? "x" : "y"] + d(m.margin, 12) + b[f])) })
            }, render: function () {
                var a = this, b = a.chart, c = b.renderer, d = a.group, h, e, r, l, t = a.box, k = a.options, w = a.padding; a.itemX = a.initialItemX; a.itemY = a.initialItemY; a.offsetWidth = 0; a.lastItemY = 0; d || (a.group = d = c.g("legend").attr({ zIndex: 7 }).add(), a.contentGroup = c.g().attr({ zIndex: 1 }).add(d), a.scrollGroup = c.g().add(a.contentGroup)); a.renderTitle();
                h = a.getAllItems(); n(h, function (a, b) { return (a.options && a.options.legendIndex || 0) - (b.options && b.options.legendIndex || 0) }); k.reversed && h.reverse(); a.allItems = h; a.display = e = !!h.length; a.lastLineHeight = 0; f(h, function (b) { a.renderItem(b) }); r = (k.width || a.offsetWidth) + w; l = a.lastItemY + a.lastLineHeight + a.titleHeight; l = a.handleOverflow(l); l += w; t || (a.box = t = c.rect().addClass("highcharts-legend-box").attr({ r: k.borderRadius }).add(d), t.isNew = !0); t.attr({
                    stroke: k.borderColor, "stroke-width": k.borderWidth || 0, fill: k.backgroundColor ||
                        "none"
                }).shadow(k.shadow); 0 < r && 0 < l && (t[t.isNew ? "attr" : "animate"](t.crisp({ x: 0, y: 0, width: r, height: l }, t.strokeWidth())), t.isNew = !1); t[e ? "show" : "hide"](); a.legendWidth = r; a.legendHeight = l; f(h, function (b) { a.positionItem(b) }); e && d.align(p({ width: r, height: l }, k), !0, "spacingBox"); b.isResizing || this.positionCheckboxes()
            }, handleOverflow: function (a) {
                var b = this, c = this.chart, m = c.renderer, h = this.options, e = h.y, c = c.spacingBox.height + ("top" === h.verticalAlign ? -e : e) - this.padding, e = h.maxHeight, n, l = this.clipRect, t = h.navigation,
                k = d(t.animation, !0), w = t.arrowSize || 12, p = this.nav, u = this.pages, y = this.padding, g, B = this.allItems, v = function (a) { a ? l.attr({ height: a }) : l && (b.clipRect = l.destroy(), b.contentGroup.clip()); b.contentGroup.div && (b.contentGroup.div.style.clip = a ? "rect(" + y + "px,9999px," + (y + a) + "px,0)" : "auto") }; "horizontal" !== h.layout || "middle" === h.verticalAlign || h.floating || (c /= 2); e && (c = Math.min(c, e)); u.length = 0; a > c && !1 !== t.enabled ? (this.clipHeight = n = Math.max(c - 20 - this.titleHeight - y, 0), this.currentPage = d(this.currentPage, 1), this.fullHeight =
                    a, f(B, function (a, b) { var c = a._legendItemPos[1]; a = Math.round(a.legendItem.getBBox().height); var k = u.length; if (!k || c - u[k - 1] > n && (g || c) !== u[k - 1]) u.push(g || c), k++; b === B.length - 1 && c + a - u[k - 1] > n && u.push(c); c !== g && (g = c) }), l || (l = b.clipRect = m.clipRect(0, y, 9999, 0), b.contentGroup.clip(l)), v(n), p || (this.nav = p = m.g().attr({ zIndex: 1 }).add(this.group), this.up = m.symbol("triangle", 0, 0, w, w).on("click", function () { b.scroll(-1, k) }).add(p), this.pager = m.text("", 15, 10).addClass("highcharts-legend-navigation").css(t.style).add(p),
                        this.down = m.symbol("triangle-down", 0, 0, w, w).on("click", function () { b.scroll(1, k) }).add(p)), b.scroll(0), a = c) : p && (v(), p.hide(), this.scrollGroup.attr({ translateY: 1 }), this.clipHeight = 0); return a
            }, scroll: function (a, b) {
                var d = this.pages, f = d.length; a = this.currentPage + a; var m = this.clipHeight, e = this.options.navigation, h = this.pager, n = this.padding; a > f && (a = f); 0 < a && (void 0 !== b && c(b, this.chart), this.nav.attr({ translateX: n, translateY: m + this.padding + 7 + this.titleHeight, visibility: "visible" }), this.up.attr({
                    "class": 1 ===
                        a ? "highcharts-legend-nav-inactive" : "highcharts-legend-nav-active"
                }), h.attr({ text: a + "/" + f }), this.down.attr({ x: 18 + this.pager.getBBox().width, "class": a === f ? "highcharts-legend-nav-inactive" : "highcharts-legend-nav-active" }), this.up.attr({ fill: 1 === a ? e.inactiveColor : e.activeColor }).css({ cursor: 1 === a ? "default" : "pointer" }), this.down.attr({ fill: a === f ? e.inactiveColor : e.activeColor }).css({ cursor: a === f ? "default" : "pointer" }), b = -d[a - 1] + this.initialItemY, this.scrollGroup.animate({ translateY: b }), this.currentPage =
                    a, this.positionCheckboxes(b))
            }
        }; a.LegendSymbolMixin = {
            drawRectangle: function (a, b) { var c = a.options, f = c.symbolHeight || a.fontMetrics.f, c = c.squareSymbol; b.legendSymbol = this.chart.renderer.rect(c ? (a.symbolWidth - f) / 2 : 0, a.baseline - f + 1, c ? f : a.symbolWidth, f, d(a.options.symbolRadius, f / 2)).addClass("highcharts-point").attr({ zIndex: 3 }).add(b.legendGroup) }, drawLineMarker: function (a) {
                var b = this.options, c = b.marker, d = a.symbolWidth, f = this.chart.renderer, e = this.legendGroup; a = a.baseline - Math.round(.3 * a.fontMetrics.b);
                var m; m = { "stroke-width": b.lineWidth || 0 }; b.dashStyle && (m.dashstyle = b.dashStyle); this.legendLine = f.path(["M", 0, a, "L", d, a]).addClass("highcharts-graph").attr(m).add(e); c && !1 !== c.enabled && (b = 0 === this.symbol.indexOf("url") ? 0 : c.radius, this.legendSymbol = c = f.symbol(this.symbol, d / 2 - b, a - b, 2 * b, 2 * b, c).addClass("highcharts-point").add(e), c.isMarker = !0)
            }
        }; (/Trident\/7\.0/.test(y.navigator.userAgent) || v) && t(D.prototype, "positionItem", function (a, b) { var c = this, d = function () { b._legendItemPos && a.call(c, b) }; d(); setTimeout(d) })
    })(L);
    (function (a) {
        var D = a.addEvent, C = a.animate, G = a.animObject, I = a.attr, h = a.doc, f = a.Axis, p = a.createElement, v = a.defaultOptions, l = a.discardElement, u = a.charts, d = a.css, c = a.defined, n = a.each, y = a.extend, t = a.find, m = a.fireEvent, b = a.getStyle, q = a.grep, z = a.isNumber, F = a.isObject, e = a.isString, r = a.Legend, x = a.marginNames, A = a.merge, k = a.Pointer, w = a.pick, K = a.pInt, J = a.removeEvent, N = a.seriesTypes, g = a.splat, B = a.svg, S = a.syncTimeout, M = a.win, R = a.Renderer, E = a.Chart = function () { this.getArgs.apply(this, arguments) }; a.chart = function (a,
            b, c) { return new E(a, b, c) }; E.prototype = {
                callbacks: [], getArgs: function () { var a = [].slice.call(arguments); if (e(a[0]) || a[0].nodeName) this.renderTo = a.shift(); this.init(a[0], a[1]) }, init: function (b, c) {
                    var k, g = b.series; b.series = null; k = A(v, b); k.series = b.series = g; this.userOptions = b; this.respRules = []; b = k.chart; g = b.events; this.margin = []; this.spacing = []; this.bounds = { h: {}, v: {} }; this.callback = c; this.isResizing = 0; this.options = k; this.axes = []; this.series = []; this.hasCartesianSeries = b.showAxes; var e; this.index = u.length;
                    u.push(this); a.chartCount++; if (g) for (e in g) D(this, e, g[e]); this.xAxis = []; this.yAxis = []; this.pointCount = this.colorCounter = this.symbolCounter = 0; this.firstRender()
                }, initSeries: function (b) { var c = this.options.chart; (c = N[b.type || c.type || c.defaultSeriesType]) || a.error(17, !0); c = new c; c.init(this, b); return c }, isInsidePlot: function (a, b, c) { var k = c ? b : a; a = c ? a : b; return 0 <= k && k <= this.plotWidth && 0 <= a && a <= this.plotHeight }, redraw: function (b) {
                    var c = this.axes, k = this.series, g = this.pointer, e = this.legend, d = this.isDirtyLegend,
                    f, h, w = this.hasCartesianSeries, r = this.isDirtyBox, l = k.length, q = l, t = this.renderer, p = t.isHidden(), H = []; a.setAnimation(b, this); p && this.cloneRenderTo(); for (this.layOutTitles(); q--;)if (b = k[q], b.options.stacking && (f = !0, b.isDirty)) { h = !0; break } if (h) for (q = l; q--;)b = k[q], b.options.stacking && (b.isDirty = !0); n(k, function (a) { a.isDirty && "point" === a.options.legendType && (a.updateTotals && a.updateTotals(), d = !0); a.isDirtyData && m(a, "updatedData") }); d && e.options.enabled && (e.render(), this.isDirtyLegend = !1); f && this.getStacks();
                    w && n(c, function (a) { a.updateNames(); a.setScale() }); this.getMargins(); w && (n(c, function (a) { a.isDirty && (r = !0) }), n(c, function (a) { var b = a.min + "," + a.max; a.extKey !== b && (a.extKey = b, H.push(function () { m(a, "afterSetExtremes", y(a.eventArgs, a.getExtremes())); delete a.eventArgs })); (r || f) && a.redraw() })); r && this.drawChartBox(); n(k, function (a) { (r || a.isDirty) && a.visible && a.redraw() }); g && g.reset(!0); t.draw(); m(this, "redraw"); p && this.cloneRenderTo(!0); n(H, function (a) { a.call() })
                }, get: function (a) {
                    function b(b) {
                        return b.id ===
                            a || b.options.id === a
                    } var c, k = this.series, g; c = t(this.axes, b) || t(this.series, b); for (g = 0; !c && g < k.length; g++)c = t(k[g].points || [], b); return c
                }, getAxes: function () { var a = this, b = this.options, c = b.xAxis = g(b.xAxis || {}), b = b.yAxis = g(b.yAxis || {}); n(c, function (a, b) { a.index = b; a.isX = !0 }); n(b, function (a, b) { a.index = b }); c = c.concat(b); n(c, function (b) { new f(a, b) }) }, getSelectedPoints: function () { var a = []; n(this.series, function (b) { a = a.concat(q(b.points || [], function (a) { return a.selected })) }); return a }, getSelectedSeries: function () {
                    return q(this.series,
                        function (a) { return a.selected })
                }, setTitle: function (a, b, c) {
                    var k = this, g = k.options, e; e = g.title = A({ style: { color: "#333333", fontSize: g.isStock ? "16px" : "18px" } }, g.title, a); g = g.subtitle = A({ style: { color: "#666666" } }, g.subtitle, b); n([["title", a, e], ["subtitle", b, g]], function (a, b) {
                        var c = a[0], g = k[c], e = a[1]; a = a[2]; g && e && (k[c] = g = g.destroy()); a && a.text && !g && (k[c] = k.renderer.text(a.text, 0, 0, a.useHTML).attr({ align: a.align, "class": "highcharts-" + c, zIndex: a.zIndex || 4 }).add(), k[c].update = function (a) {
                            k.setTitle(!b && a, b &&
                                a)
                        }, k[c].css(a.style))
                    }); k.layOutTitles(c)
                }, layOutTitles: function (a) {
                    var b = 0, c, k = this.renderer, g = this.spacingBox; n(["title", "subtitle"], function (a) { var c = this[a], e = this.options[a], d; c && (d = e.style.fontSize, d = k.fontMetrics(d, c).b, c.css({ width: (e.width || g.width + e.widthAdjust) + "px" }).align(y({ y: b + d + ("title" === a ? -3 : 2) }, e), !1, "spacingBox"), e.floating || e.verticalAlign || (b = Math.ceil(b + c.getBBox().height))) }, this); c = this.titleOffset !== b; this.titleOffset = b; !this.isDirtyBox && c && (this.isDirtyBox = c, this.hasRendered &&
                        w(a, !0) && this.isDirtyBox && this.redraw())
                }, getChartSize: function () { var a = this.options.chart, k = a.width, a = a.height, g = this.renderToClone || this.renderTo; c(k) || (this.containerWidth = b(g, "width")); c(a) || (this.containerHeight = b(g, "height")); this.chartWidth = Math.max(0, k || this.containerWidth || 600); this.chartHeight = Math.max(0, w(a, 19 < this.containerHeight ? this.containerHeight : 400)) }, cloneRenderTo: function (a) {
                    var b = this.renderToClone, c = this.container; if (a) {
                        if (b) {
                            for (; b.childNodes.length;)this.renderTo.appendChild(b.firstChild);
                            l(b); delete this.renderToClone
                        }
                    } else c && c.parentNode === this.renderTo && this.renderTo.removeChild(c), this.renderToClone = b = this.renderTo.cloneNode(0), d(b, { position: "absolute", top: "-9999px", display: "block" }), b.style.setProperty && b.style.setProperty("display", "block", "important"), h.body.appendChild(b), c && b.appendChild(c)
                }, setClassName: function (a) { this.container.className = "highcharts-container " + (a || "") }, getContainer: function () {
                    var b, c = this.options, k = c.chart, g, d; b = this.renderTo; var f = a.uniqueKey(), m; b ||
                        (this.renderTo = b = k.renderTo); e(b) && (this.renderTo = b = h.getElementById(b)); b || a.error(13, !0); g = K(I(b, "data-highcharts-chart")); z(g) && u[g] && u[g].hasRendered && u[g].destroy(); I(b, "data-highcharts-chart", this.index); b.innerHTML = ""; k.skipClone || b.offsetWidth || this.cloneRenderTo(); this.getChartSize(); g = this.chartWidth; d = this.chartHeight; m = y({ position: "relative", overflow: "hidden", width: g + "px", height: d + "px", textAlign: "left", lineHeight: "normal", zIndex: 0, "-webkit-tap-highlight-color": "rgba(0,0,0,0)" }, k.style);
                    this.container = b = p("div", { id: f }, m, this.renderToClone || b); this._cursor = b.style.cursor; this.renderer = new (a[k.renderer] || R)(b, g, d, null, k.forExport, c.exporting && c.exporting.allowHTML); this.setClassName(k.className); this.renderer.setStyle(k.style); this.renderer.chartIndex = this.index
                }, getMargins: function (a) {
                    var b = this.spacing, k = this.margin, g = this.titleOffset; this.resetMargins(); g && !c(k[0]) && (this.plotTop = Math.max(this.plotTop, g + this.options.title.margin + b[0])); this.legend.display && this.legend.adjustMargins(k,
                        b); this.extraBottomMargin && (this.marginBottom += this.extraBottomMargin); this.extraTopMargin && (this.plotTop += this.extraTopMargin); a || this.getAxisMargins()
                }, getAxisMargins: function () { var a = this, b = a.axisOffset = [0, 0, 0, 0], k = a.margin; a.hasCartesianSeries && n(a.axes, function (a) { a.visible && a.getOffset() }); n(x, function (g, e) { c(k[e]) || (a[g] += b[e]) }); a.setChartSize() }, reflow: function (a) {
                    var k = this, g = k.options.chart, e = k.renderTo, d = c(g.width), f = g.width || b(e, "width"), g = g.height || b(e, "height"), e = a ? a.target : M; if (!d &&
                        !k.isPrinting && f && g && (e === M || e === h)) { if (f !== k.containerWidth || g !== k.containerHeight) clearTimeout(k.reflowTimeout), k.reflowTimeout = S(function () { k.container && k.setSize(void 0, void 0, !1) }, a ? 100 : 0); k.containerWidth = f; k.containerHeight = g }
                }, initReflow: function () { var a = this, b; b = D(M, "resize", function (b) { a.reflow(b) }); D(a, "destroy", b) }, setSize: function (b, c, k) {
                    var g = this, e = g.renderer; g.isResizing += 1; a.setAnimation(k, g); g.oldChartHeight = g.chartHeight; g.oldChartWidth = g.chartWidth; void 0 !== b && (g.options.chart.width =
                        b); void 0 !== c && (g.options.chart.height = c); g.getChartSize(); b = e.globalAnimation; (b ? C : d)(g.container, { width: g.chartWidth + "px", height: g.chartHeight + "px" }, b); g.setChartSize(!0); e.setSize(g.chartWidth, g.chartHeight, k); n(g.axes, function (a) { a.isDirty = !0; a.setScale() }); g.isDirtyLegend = !0; g.isDirtyBox = !0; g.layOutTitles(); g.getMargins(); g.setResponsive && g.setResponsive(!1); g.redraw(k); g.oldChartHeight = null; m(g, "resize"); S(function () { g && m(g, "endResize", null, function () { --g.isResizing }) }, G(b).duration)
                }, setChartSize: function (a) {
                    var b =
                        this.inverted, c = this.renderer, g = this.chartWidth, k = this.chartHeight, e = this.options.chart, d = this.spacing, f = this.clipOffset, m, h, w, r; this.plotLeft = m = Math.round(this.plotLeft); this.plotTop = h = Math.round(this.plotTop); this.plotWidth = w = Math.max(0, Math.round(g - m - this.marginRight)); this.plotHeight = r = Math.max(0, Math.round(k - h - this.marginBottom)); this.plotSizeX = b ? r : w; this.plotSizeY = b ? w : r; this.plotBorderWidth = e.plotBorderWidth || 0; this.spacingBox = c.spacingBox = { x: d[3], y: d[0], width: g - d[3] - d[1], height: k - d[0] - d[2] };
                    this.plotBox = c.plotBox = { x: m, y: h, width: w, height: r }; g = 2 * Math.floor(this.plotBorderWidth / 2); b = Math.ceil(Math.max(g, f[3]) / 2); c = Math.ceil(Math.max(g, f[0]) / 2); this.clipBox = { x: b, y: c, width: Math.floor(this.plotSizeX - Math.max(g, f[1]) / 2 - b), height: Math.max(0, Math.floor(this.plotSizeY - Math.max(g, f[2]) / 2 - c)) }; a || n(this.axes, function (a) { a.setAxisSize(); a.setAxisTranslation() })
                }, resetMargins: function () {
                    var a = this, b = a.options.chart; n(["margin", "spacing"], function (c) {
                        var g = b[c], k = F(g) ? g : [g, g, g, g]; n(["Top", "Right",
                            "Bottom", "Left"], function (g, e) { a[c][e] = w(b[c + g], k[e]) })
                    }); n(x, function (b, c) { a[b] = w(a.margin[c], a.spacing[c]) }); a.axisOffset = [0, 0, 0, 0]; a.clipOffset = [0, 0, 0, 0]
                }, drawChartBox: function () {
                    var a = this.options.chart, b = this.renderer, c = this.chartWidth, g = this.chartHeight, k = this.chartBackground, e = this.plotBackground, d = this.plotBorder, f, m = this.plotBGImage, h = a.backgroundColor, w = a.plotBackgroundColor, n = a.plotBackgroundImage, r, l = this.plotLeft, q = this.plotTop, t = this.plotWidth, p = this.plotHeight, x = this.plotBox, K = this.clipRect,
                    u = this.clipBox, A = "animate"; k || (this.chartBackground = k = b.rect().addClass("highcharts-background").add(), A = "attr"); f = a.borderWidth || 0; r = f + (a.shadow ? 8 : 0); h = { fill: h || "none" }; if (f || k["stroke-width"]) h.stroke = a.borderColor, h["stroke-width"] = f; k.attr(h).shadow(a.shadow); k[A]({ x: r / 2, y: r / 2, width: c - r - f % 2, height: g - r - f % 2, r: a.borderRadius }); A = "animate"; e || (A = "attr", this.plotBackground = e = b.rect().addClass("highcharts-plot-background").add()); e[A](x); e.attr({ fill: w || "none" }).shadow(a.plotShadow); n && (m ? m.animate(x) :
                        this.plotBGImage = b.image(n, l, q, t, p).add()); K ? K.animate({ width: u.width, height: u.height }) : this.clipRect = b.clipRect(u); A = "animate"; d || (A = "attr", this.plotBorder = d = b.rect().addClass("highcharts-plot-border").attr({ zIndex: 1 }).add()); d.attr({ stroke: a.plotBorderColor, "stroke-width": a.plotBorderWidth || 0, fill: "none" }); d[A](d.crisp({ x: l, y: q, width: t, height: p }, -d.strokeWidth())); this.isDirtyBox = !1
                }, propFromSeries: function () {
                    var a = this, b = a.options.chart, c, g = a.options.series, k, e; n(["inverted", "angular", "polar"],
                        function (d) { c = N[b.type || b.defaultSeriesType]; e = b[d] || c && c.prototype[d]; for (k = g && g.length; !e && k--;)(c = N[g[k].type]) && c.prototype[d] && (e = !0); a[d] = e })
                }, linkSeries: function () { var a = this, b = a.series; n(b, function (a) { a.linkedSeries.length = 0 }); n(b, function (b) { var c = b.options.linkedTo; e(c) && (c = ":previous" === c ? a.series[b.index - 1] : a.get(c)) && c.linkedParent !== b && (c.linkedSeries.push(b), b.linkedParent = c, b.visible = w(b.options.visible, c.options.visible, b.visible)) }) }, renderSeries: function () {
                    n(this.series, function (a) {
                        a.translate();
                        a.render()
                    })
                }, renderLabels: function () { var a = this, b = a.options.labels; b.items && n(b.items, function (c) { var g = y(b.style, c.style), k = K(g.left) + a.plotLeft, e = K(g.top) + a.plotTop + 12; delete g.left; delete g.top; a.renderer.text(c.html, k, e).attr({ zIndex: 2 }).css(g).add() }) }, render: function () {
                    var a = this.axes, b = this.renderer, c = this.options, g, k, e; this.setTitle(); this.legend = new r(this, c.legend); this.getStacks && this.getStacks(); this.getMargins(!0); this.setChartSize(); c = this.plotWidth; g = this.plotHeight -= 21; n(a, function (a) { a.setScale() });
                    this.getAxisMargins(); k = 1.1 < c / this.plotWidth; e = 1.05 < g / this.plotHeight; if (k || e) n(a, function (a) { (a.horiz && k || !a.horiz && e) && a.setTickInterval(!0) }), this.getMargins(); this.drawChartBox(); this.hasCartesianSeries && n(a, function (a) { a.visible && a.render() }); this.seriesGroup || (this.seriesGroup = b.g("series-group").attr({ zIndex: 3 }).add()); this.renderSeries(); this.renderLabels(); this.addCredits(); this.setResponsive && this.setResponsive(); this.hasRendered = !0
                }, addCredits: function (a) {
                    var b = this; a = A(!0, this.options.credits,
                        a); a.enabled && !this.credits && (this.credits = this.renderer.text(a.text + (this.mapCredits || ""), 0, 0).addClass("highcharts-credits").on("click", function () { a.href && (M.location.href = a.href) }).attr({ align: a.position.align, zIndex: 8 }).css(a.style).add().align(a.position), this.credits.update = function (a) { b.credits = b.credits.destroy(); b.addCredits(a) })
                }, destroy: function () {
                    var b = this, c = b.axes, g = b.series, k = b.container, e, d = k && k.parentNode; m(b, "destroy"); u[b.index] = void 0; a.chartCount--; b.renderTo.removeAttribute("data-highcharts-chart");
                    J(b); for (e = c.length; e--;)c[e] = c[e].destroy(); this.scroller && this.scroller.destroy && this.scroller.destroy(); for (e = g.length; e--;)g[e] = g[e].destroy(); n("title subtitle chartBackground plotBackground plotBGImage plotBorder seriesGroup clipRect credits pointer rangeSelector legend resetZoomButton tooltip renderer".split(" "), function (a) { var c = b[a]; c && c.destroy && (b[a] = c.destroy()) }); k && (k.innerHTML = "", J(k), d && l(k)); for (e in b) delete b[e]
                }, isReadyToRender: function () {
                    var a = this; return B || M != M.top || "complete" ===
                        h.readyState ? !0 : (h.attachEvent("onreadystatechange", function () { h.detachEvent("onreadystatechange", a.firstRender); "complete" === h.readyState && a.firstRender() }), !1)
                }, firstRender: function () {
                    var a = this, b = a.options; if (a.isReadyToRender()) {
                        a.getContainer(); m(a, "init"); a.resetMargins(); a.setChartSize(); a.propFromSeries(); a.getAxes(); n(b.series || [], function (b) { a.initSeries(b) }); a.linkSeries(); m(a, "beforeRender"); k && (a.pointer = new k(a, b)); a.render(); a.renderer.draw(); if (!a.renderer.imgCount && a.onload) a.onload();
                        a.cloneRenderTo(!0)
                    }
                }, onload: function () { n([this.callback].concat(this.callbacks), function (a) { a && void 0 !== this.index && a.apply(this, [this]) }, this); m(this, "load"); c(this.index) && !1 !== this.options.chart.reflow && this.initReflow(); this.onload = null }
            }
    })(L); (function (a) {
        var D, C = a.each, G = a.extend, I = a.erase, h = a.fireEvent, f = a.format, p = a.isArray, v = a.isNumber, l = a.pick, u = a.removeEvent; D = a.Point = function () { }; D.prototype = {
            init: function (a, c, f) {
            this.series = a; this.color = a.color; this.applyOptions(c, f); a.options.colorByPoint ?
                (c = a.options.colors || a.chart.options.colors, this.color = this.color || c[a.colorCounter], c = c.length, f = a.colorCounter, a.colorCounter++, a.colorCounter === c && (a.colorCounter = 0)) : f = a.colorIndex; this.colorIndex = l(this.colorIndex, f); a.chart.pointCount++; return this
            }, applyOptions: function (a, c) {
                var d = this.series, f = d.options.pointValKey || d.pointValKey; a = D.prototype.optionsToObject.call(this, a); G(this, a); this.options = this.options ? G(this.options, a) : a; a.group && delete this.group; f && (this.y = this[f]); this.isNull = l(this.isValid &&
                    !this.isValid(), null === this.x || !v(this.y, !0)); this.selected && (this.state = "select"); "name" in this && void 0 === c && d.xAxis && d.xAxis.hasNames && (this.x = d.xAxis.nameToX(this)); void 0 === this.x && d && (this.x = void 0 === c ? d.autoIncrement(this) : c); return this
            }, optionsToObject: function (a) {
                var c = {}, d = this.series, f = d.options.keys, h = f || d.pointArrayMap || ["y"], m = h.length, b = 0, l = 0; if (v(a) || null === a) c[h[0]] = a; else if (p(a)) for (!f && a.length > m && (d = typeof a[0], "string" === d ? c.name = a[0] : "number" === d && (c.x = a[0]), b++); l < m;)f && void 0 ===
                    a[b] || (c[h[l]] = a[b]), b++, l++; else "object" === typeof a && (c = a, a.dataLabels && (d._hasPointLabels = !0), a.marker && (d._hasPointMarkers = !0)); return c
            }, getClassName: function () { return "highcharts-point" + (this.selected ? " highcharts-point-select" : "") + (this.negative ? " highcharts-negative" : "") + (this.isNull ? " highcharts-null-point" : "") + (void 0 !== this.colorIndex ? " highcharts-color-" + this.colorIndex : "") + (this.options.className ? " " + this.options.className : "") + (this.zone && this.zone.className ? " " + this.zone.className : "") },
            getZone: function () { var a = this.series, c = a.zones, a = a.zoneAxis || "y", f = 0, h; for (h = c[f]; this[a] >= h.value;)h = c[++f]; h && h.color && !this.options.color && (this.color = h.color); return h }, destroy: function () { var a = this.series.chart, c = a.hoverPoints, f; a.pointCount--; c && (this.setState(), I(c, this), c.length || (a.hoverPoints = null)); if (this === a.hoverPoint) this.onMouseOut(); if (this.graphic || this.dataLabel) u(this), this.destroyElements(); this.legendItem && a.legend.destroyItem(this); for (f in this) this[f] = null }, destroyElements: function () {
                for (var a =
                    ["graphic", "dataLabel", "dataLabelUpper", "connector", "shadowGroup"], c, f = 6; f--;)c = a[f], this[c] && (this[c] = this[c].destroy())
            }, getLabelConfig: function () { return { x: this.category, y: this.y, color: this.color, key: this.name || this.category, series: this.series, point: this, percentage: this.percentage, total: this.total || this.stackTotal } }, tooltipFormatter: function (a) {
                var c = this.series, d = c.tooltipOptions, h = l(d.valueDecimals, ""), t = d.valuePrefix || "", m = d.valueSuffix || ""; C(c.pointArrayMap || ["y"], function (b) {
                    b = "{point." + b;
                    if (t || m) a = a.replace(b + "}", t + b + "}" + m); a = a.replace(b + "}", b + ":,." + h + "f}")
                }); return f(a, { point: this, series: this.series })
            }, firePointEvent: function (a, c, f) { var d = this, n = this.series.options; (n.point.events[a] || d.options && d.options.events && d.options.events[a]) && this.importEvents(); "click" === a && n.allowPointSelect && (f = function (a) { d.select && d.select(null, a.ctrlKey || a.metaKey || a.shiftKey) }); h(this, a, c, f) }, visible: !0
        }
    })(L); (function (a) {
        var D = a.addEvent, C = a.animObject, G = a.arrayMax, I = a.arrayMin, h = a.correctFloat,
        f = a.Date, p = a.defaultOptions, v = a.defaultPlotOptions, l = a.defined, u = a.each, d = a.erase, c = a.extend, n = a.fireEvent, y = a.grep, t = a.isArray, m = a.isNumber, b = a.isString, q = a.merge, z = a.pick, F = a.removeEvent, e = a.splat, r = a.SVGElement, x = a.syncTimeout, A = a.win; a.Series = a.seriesType("line", null, {
            lineWidth: 2, allowPointSelect: !1, showCheckbox: !1, animation: { duration: 1E3 }, events: {}, marker: {
                lineWidth: 0, lineColor: "#ffffff", radius: 4, states: {
                    hover: { animation: { duration: 50 }, enabled: !0, radiusPlus: 2, lineWidthPlus: 1 }, select: {
                        fillColor: "#cccccc",
                        lineColor: "#000000", lineWidth: 2
                    }
                }
            }, point: { events: {} }, dataLabels: { align: "center", formatter: function () { return null === this.y ? "" : a.numberFormat(this.y, -1) }, style: { fontSize: "11px", fontWeight: "bold", color: "contrast", textOutline: "1px contrast" }, verticalAlign: "bottom", x: 0, y: 0, padding: 5 }, cropThreshold: 300, pointRange: 0, softThreshold: !0, states: { hover: { lineWidthPlus: 1, marker: {}, halo: { size: 10, opacity: .25 } }, select: { marker: {} } }, stickyTracking: !0, turboThreshold: 1E3
        }, {
            isCartesian: !0, pointClass: a.Point, sorted: !0, requireSorting: !0,
            directTouch: !1, axisTypes: ["xAxis", "yAxis"], colorCounter: 0, parallelArrays: ["x", "y"], coll: "series", init: function (a, b) {
                var k = this, e, d, g = a.series, f; k.chart = a; k.options = b = k.setOptions(b); k.linkedSeries = []; k.bindAxes(); c(k, { name: b.name, state: "", visible: !1 !== b.visible, selected: !0 === b.selected }); d = b.events; for (e in d) D(k, e, d[e]); if (d && d.click || b.point && b.point.events && b.point.events.click || b.allowPointSelect) a.runTrackerClick = !0; k.getColor(); k.getSymbol(); u(k.parallelArrays, function (a) { k[a + "Data"] = [] }); k.setData(b.data,
                    !1); k.isCartesian && (a.hasCartesianSeries = !0); g.length && (f = g[g.length - 1]); k._i = z(f && f._i, -1) + 1; for (a = this.insert(g); a < g.length; a++)g[a].index = a, g[a].name = g[a].name || "Series " + (g[a].index + 1)
            }, insert: function (a) { var b = this.options.index, c; if (m(b)) { for (c = a.length; c--;)if (b >= z(a[c].options.index, a[c]._i)) { a.splice(c + 1, 0, this); break } -1 === c && a.unshift(this); c += 1 } else a.push(this); return z(c, a.length - 1) }, bindAxes: function () {
                var b = this, c = b.options, e = b.chart, d; u(b.axisTypes || [], function (k) {
                    u(e[k], function (a) {
                        d =
                        a.options; if (c[k] === d.index || void 0 !== c[k] && c[k] === d.id || void 0 === c[k] && 0 === d.index) b.insert(a.series), b[k] = a, a.isDirty = !0
                    }); b[k] || b.optionalAxis === k || a.error(18, !0)
                })
            }, updateParallelArrays: function (a, b) { var c = a.series, k = arguments, e = m(b) ? function (g) { var k = "y" === g && c.toYData ? c.toYData(a) : a[g]; c[g + "Data"][b] = k } : function (a) { Array.prototype[b].apply(c[a + "Data"], Array.prototype.slice.call(k, 2)) }; u(c.parallelArrays, e) }, autoIncrement: function () {
                var a = this.options, b = this.xIncrement, c, e = a.pointIntervalUnit,
                b = z(b, a.pointStart, 0); this.pointInterval = c = z(this.pointInterval, a.pointInterval, 1); e && (a = new f(b), "day" === e ? a = +a[f.hcSetDate](a[f.hcGetDate]() + c) : "month" === e ? a = +a[f.hcSetMonth](a[f.hcGetMonth]() + c) : "year" === e && (a = +a[f.hcSetFullYear](a[f.hcGetFullYear]() + c)), c = a - b); this.xIncrement = b + c; return b
            }, setOptions: function (a) {
                var b = this.chart, c = b.options.plotOptions, b = b.userOptions || {}, k = b.plotOptions || {}, e = c[this.type]; this.userOptions = a; c = q(e, c.series, a); this.tooltipOptions = q(p.tooltip, p.plotOptions[this.type].tooltip,
                    b.tooltip, k.series && k.series.tooltip, k[this.type] && k[this.type].tooltip, a.tooltip); null === e.marker && delete c.marker; this.zoneAxis = c.zoneAxis; a = this.zones = (c.zones || []).slice(); !c.negativeColor && !c.negativeFillColor || c.zones || a.push({ value: c[this.zoneAxis + "Threshold"] || c.threshold || 0, className: "highcharts-negative", color: c.negativeColor, fillColor: c.negativeFillColor }); a.length && l(a[a.length - 1].value) && a.push({ color: this.color, fillColor: this.fillColor }); return c
            }, getCyclic: function (a, b, c) {
                var k, e = this.userOptions,
                g = a + "Index", d = a + "Counter", f = c ? c.length : z(this.chart.options.chart[a + "Count"], this.chart[a + "Count"]); b || (k = z(e[g], e["_" + g]), l(k) || (e["_" + g] = k = this.chart[d] % f, this.chart[d] += 1), c && (b = c[k])); void 0 !== k && (this[g] = k); this[a] = b
            }, getColor: function () { this.options.colorByPoint ? this.options.color = null : this.getCyclic("color", this.options.color || v[this.type].color, this.chart.options.colors) }, getSymbol: function () { this.getCyclic("symbol", this.options.marker.symbol, this.chart.options.symbols) }, drawLegendSymbol: a.LegendSymbolMixin.drawLineMarker,
            setData: function (c, e, d, f) {
                var k = this, g = k.points, h = g && g.length || 0, n, r = k.options, w = k.chart, l = null, q = k.xAxis, p = r.turboThreshold, x = this.xData, A = this.yData, F = (n = k.pointArrayMap) && n.length; c = c || []; n = c.length; e = z(e, !0); if (!1 !== f && n && h === n && !k.cropped && !k.hasGroupedData && k.visible) u(c, function (a, b) { g[b].update && a !== r.data[b] && g[b].update(a, !1, null, !1) }); else {
                k.xIncrement = null; k.colorCounter = 0; u(this.parallelArrays, function (a) { k[a + "Data"].length = 0 }); if (p && n > p) {
                    for (d = 0; null === l && d < n;)l = c[d], d++; if (m(l)) for (d =
                        0; d < n; d++)x[d] = this.autoIncrement(), A[d] = c[d]; else if (t(l)) if (F) for (d = 0; d < n; d++)l = c[d], x[d] = l[0], A[d] = l.slice(1, F + 1); else for (d = 0; d < n; d++)l = c[d], x[d] = l[0], A[d] = l[1]; else a.error(12)
                } else for (d = 0; d < n; d++)void 0 !== c[d] && (l = { series: k }, k.pointClass.prototype.applyOptions.apply(l, [c[d]]), k.updateParallelArrays(l, d)); b(A[0]) && a.error(14, !0); k.data = []; k.options.data = k.userOptions.data = c; for (d = h; d--;)g[d] && g[d].destroy && g[d].destroy(); q && (q.minRange = q.userMinRange); k.isDirty = w.isDirtyBox = !0; k.isDirtyData =
                    !!g; d = !1
                } "point" === r.legendType && (this.processData(), this.generatePoints()); e && w.redraw(d)
            }, processData: function (b) {
                var c = this.xData, k = this.yData, e = c.length, d; d = 0; var g, f, m = this.xAxis, h, n = this.options; h = n.cropThreshold; var l = this.getExtremesFromAll || n.getExtremesFromAll, r = this.isCartesian, n = m && m.val2lin, q = m && m.isLog, t, p; if (r && !this.isDirty && !m.isDirty && !this.yAxis.isDirty && !b) return !1; m && (b = m.getExtremes(), t = b.min, p = b.max); if (r && this.sorted && !l && (!h || e > h || this.forceCrop)) if (c[e - 1] < t || c[0] > p) c = [],
                    k = []; else if (c[0] < t || c[e - 1] > p) d = this.cropData(this.xData, this.yData, t, p), c = d.xData, k = d.yData, d = d.start, g = !0; for (h = c.length || 1; --h;)e = q ? n(c[h]) - n(c[h - 1]) : c[h] - c[h - 1], 0 < e && (void 0 === f || e < f) ? f = e : 0 > e && this.requireSorting && a.error(15); this.cropped = g; this.cropStart = d; this.processedXData = c; this.processedYData = k; this.closestPointRange = f
            }, cropData: function (a, b, c, e) {
                var k = a.length, g = 0, d = k, f = z(this.cropShoulder, 1), h; for (h = 0; h < k; h++)if (a[h] >= c) { g = Math.max(0, h - f); break } for (c = h; c < k; c++)if (a[c] > e) { d = c + f; break } return {
                    xData: a.slice(g,
                        d), yData: b.slice(g, d), start: g, end: d
                }
            }, generatePoints: function () {
                var a = this.options.data, b = this.data, c, d = this.processedXData, f = this.processedYData, g = this.pointClass, h = d.length, m = this.cropStart || 0, n, r = this.hasGroupedData, l, q = [], t; b || r || (b = [], b.length = a.length, b = this.data = b); for (t = 0; t < h; t++)n = m + t, r ? (l = (new g).init(this, [d[t]].concat(e(f[t]))), l.dataGroup = this.groupMap[t]) : (l = b[n]) || void 0 === a[n] || (b[n] = l = (new g).init(this, a[n], d[t])), l.index = n, q[t] = l; if (b && (h !== (c = b.length) || r)) for (t = 0; t < c; t++)t !== m ||
                    r || (t += h), b[t] && (b[t].destroyElements(), b[t].plotX = void 0); this.data = b; this.points = q
            }, getExtremes: function (a) {
                var b = this.yAxis, c = this.processedXData, e, k = [], g = 0; e = this.xAxis.getExtremes(); var d = e.min, f = e.max, h, n, l, r; a = a || this.stackedYData || this.processedYData || []; e = a.length; for (r = 0; r < e; r++)if (n = c[r], l = a[r], h = (m(l, !0) || t(l)) && (!b.isLog || l.length || 0 < l), n = this.getExtremesFromAll || this.options.getExtremesFromAll || this.cropped || (c[r + 1] || n) >= d && (c[r - 1] || n) <= f, h && n) if (h = l.length) for (; h--;)null !== l[h] && (k[g++] =
                    l[h]); else k[g++] = l; this.dataMin = I(k); this.dataMax = G(k)
            }, translate: function () {
            this.processedXData || this.processData(); this.generatePoints(); var a = this.options, b = a.stacking, c = this.xAxis, e = c.categories, d = this.yAxis, g = this.points, f = g.length, n = !!this.modifyValue, r = a.pointPlacement, t = "between" === r || m(r), q = a.threshold, p = a.startFromThreshold ? q : 0, x, u, A, F, v = Number.MAX_VALUE; "between" === r && (r = .5); m(r) && (r *= z(a.pointRange || c.pointRange)); for (a = 0; a < f; a++) {
                var y = g[a], C = y.x, D = y.y; u = y.low; var G = b && d.stacks[(this.negStacks &&
                    D < (p ? 0 : q) ? "-" : "") + this.stackKey], I; d.isLog && null !== D && 0 >= D && (y.isNull = !0); y.plotX = x = h(Math.min(Math.max(-1E5, c.translate(C, 0, 0, 0, 1, r, "flags" === this.type)), 1E5)); b && this.visible && !y.isNull && G && G[C] && (F = this.getStackIndicator(F, C, this.index), I = G[C], D = I.points[F.key], u = D[0], D = D[1], u === p && F.key === G[C].base && (u = z(q, d.min)), d.isLog && 0 >= u && (u = null), y.total = y.stackTotal = I.total, y.percentage = I.total && y.y / I.total * 100, y.stackY = D, I.setOffset(this.pointXOffset || 0, this.barW || 0)); y.yBottom = l(u) ? d.translate(u, 0,
                        1, 0, 1) : null; n && (D = this.modifyValue(D, y)); y.plotY = u = "number" === typeof D && Infinity !== D ? Math.min(Math.max(-1E5, d.translate(D, 0, 1, 0, 1)), 1E5) : void 0; y.isInside = void 0 !== u && 0 <= u && u <= d.len && 0 <= x && x <= c.len; y.clientX = t ? h(c.translate(C, 0, 0, 0, 1, r)) : x; y.negative = y.y < (q || 0); y.category = e && void 0 !== e[y.x] ? e[y.x] : y.x; y.isNull || (void 0 !== A && (v = Math.min(v, Math.abs(x - A))), A = x); y.zone = this.zones.length && y.getZone()
            } this.closestPointRangePx = v
            }, getValidPoints: function (a, b) {
                var c = this.chart; return y(a || this.points || [],
                    function (a) { return b && !c.isInsidePlot(a.plotX, a.plotY, c.inverted) ? !1 : !a.isNull })
            }, setClip: function (a) {
                var b = this.chart, c = this.options, e = b.renderer, k = b.inverted, g = this.clipBox, d = g || b.clipBox, f = this.sharedClipKey || ["_sharedClip", a && a.duration, a && a.easing, d.height, c.xAxis, c.yAxis].join(), h = b[f], m = b[f + "m"]; h || (a && (d.width = 0, b[f + "m"] = m = e.clipRect(-99, k ? -b.plotLeft : -b.plotTop, 99, k ? b.chartWidth : b.chartHeight)), b[f] = h = e.clipRect(d), h.count = { length: 0 }); a && !h.count[this.index] && (h.count[this.index] = !0, h.count.length +=
                    1); !1 !== c.clip && (this.group.clip(a || g ? h : b.clipRect), this.markerGroup.clip(m), this.sharedClipKey = f); a || (h.count[this.index] && (delete h.count[this.index], --h.count.length), 0 === h.count.length && f && b[f] && (g || (b[f] = b[f].destroy()), b[f + "m"] && (b[f + "m"] = b[f + "m"].destroy())))
            }, animate: function (a) { var b = this.chart, c = C(this.options.animation), e; a ? this.setClip(c) : (e = this.sharedClipKey, (a = b[e]) && a.animate({ width: b.plotSizeX }, c), b[e + "m"] && b[e + "m"].animate({ width: b.plotSizeX + 99 }, c), this.animate = null) }, afterAnimate: function () {
                this.setClip();
                n(this, "afterAnimate")
            }, drawPoints: function () {
                var a = this.points, b = this.chart, c, e, d, g, f = this.options.marker, h, n, r, l, t = this.markerGroup, q = z(f.enabled, this.xAxis.isRadial ? !0 : null, this.closestPointRangePx > 2 * f.radius); if (!1 !== f.enabled || this._hasPointMarkers) for (e = a.length; e--;)d = a[e], c = d.plotY, g = d.graphic, h = d.marker || {}, n = !!d.marker, r = q && void 0 === h.enabled || h.enabled, l = d.isInside, r && m(c) && null !== d.y ? (c = z(h.symbol, this.symbol), d.hasImage = 0 === c.indexOf("url"), r = this.markerAttribs(d, d.selected && "select"),
                    g ? g[l ? "show" : "hide"](!0).animate(r) : l && (0 < r.width || d.hasImage) && (d.graphic = g = b.renderer.symbol(c, r.x, r.y, r.width, r.height, n ? h : f).add(t)), g && g.attr(this.pointAttribs(d, d.selected && "select")), g && g.addClass(d.getClassName(), !0)) : g && (d.graphic = g.destroy())
            }, markerAttribs: function (a, b) {
                var c = this.options.marker, e = a && a.options, k = e && e.marker || {}, e = z(k.radius, c.radius); b && (c = c.states[b], b = k.states && k.states[b], e = z(b && b.radius, c && c.radius, e + (c && c.radiusPlus || 0))); a.hasImage && (e = 0); a = {
                    x: Math.floor(a.plotX) -
                        e, y: a.plotY - e
                }; e && (a.width = a.height = 2 * e); return a
            }, pointAttribs: function (a, b) {
                var c = this.options.marker, e = a && a.options, k = e && e.marker || {}, g = this.color, d = e && e.color, f = a && a.color, e = z(k.lineWidth, c.lineWidth); a = a && a.zone && a.zone.color; g = d || a || f || g; a = k.fillColor || c.fillColor || g; g = k.lineColor || c.lineColor || g; b && (c = c.states[b], b = k.states && k.states[b] || {}, e = z(b.lineWidth, c.lineWidth, e + z(b.lineWidthPlus, c.lineWidthPlus, 0)), a = b.fillColor || c.fillColor || a, g = b.lineColor || c.lineColor || g); return {
                    stroke: g, "stroke-width": e,
                    fill: a
                }
            }, destroy: function () {
                var a = this, b = a.chart, c = /AppleWebKit\/533/.test(A.navigator.userAgent), e, f = a.data || [], g, h, m; n(a, "destroy"); F(a); u(a.axisTypes || [], function (b) { (m = a[b]) && m.series && (d(m.series, a), m.isDirty = m.forceRedraw = !0) }); a.legendItem && a.chart.legend.destroyItem(a); for (e = f.length; e--;)(g = f[e]) && g.destroy && g.destroy(); a.points = null; clearTimeout(a.animationTimeout); for (h in a) a[h] instanceof r && !a[h].survive && (e = c && "group" === h ? "hide" : "destroy", a[h][e]()); b.hoverSeries === a && (b.hoverSeries =
                    null); d(b.series, a); for (h in a) delete a[h]
            }, getGraphPath: function (a, b, c) {
                var e = this, k = e.options, g = k.step, d, f = [], h = [], m; a = a || e.points; (d = a.reversed) && a.reverse(); (g = { right: 1, center: 2 }[g] || g && 3) && d && (g = 4 - g); !k.connectNulls || b || c || (a = this.getValidPoints(a)); u(a, function (d, n) {
                    var r = d.plotX, t = d.plotY, q = a[n - 1]; (d.leftCliff || q && q.rightCliff) && !c && (m = !0); d.isNull && !l(b) && 0 < n ? m = !k.connectNulls : d.isNull && !b ? m = !0 : (0 === n || m ? n = ["M", d.plotX, d.plotY] : e.getPointSpline ? n = e.getPointSpline(a, d, n) : g ? (n = 1 === g ? ["L", q.plotX,
                        t] : 2 === g ? ["L", (q.plotX + r) / 2, q.plotY, "L", (q.plotX + r) / 2, t] : ["L", r, q.plotY], n.push("L", r, t)) : n = ["L", r, t], h.push(d.x), g && h.push(d.x), f.push.apply(f, n), m = !1)
                }); f.xMap = h; return e.graphPath = f
            }, drawGraph: function () {
                var a = this, b = this.options, c = (this.gappedPath || this.getGraphPath).call(this), e = [["graph", "highcharts-graph", b.lineColor || this.color, b.dashStyle]]; u(this.zones, function (c, g) { e.push(["zone-graph-" + g, "highcharts-graph highcharts-zone-graph-" + g + " " + (c.className || ""), c.color || a.color, c.dashStyle || b.dashStyle]) });
                u(e, function (e, g) { var k = e[0], d = a[k]; d ? (d.endX = c.xMap, d.animate({ d: c })) : c.length && (a[k] = a.chart.renderer.path(c).addClass(e[1]).attr({ zIndex: 1 }).add(a.group), d = { stroke: e[2], "stroke-width": b.lineWidth, fill: a.fillGraph && a.color || "none" }, e[3] ? d.dashstyle = e[3] : "square" !== b.linecap && (d["stroke-linecap"] = d["stroke-linejoin"] = "round"), d = a[k].attr(d).shadow(2 > g && b.shadow)); d && (d.startX = c.xMap, d.isArea = c.isArea) })
            }, applyZones: function () {
                var a = this, b = this.chart, c = b.renderer, e = this.zones, d, g, f = this.clips || [],
                h, m = this.graph, n = this.area, r = Math.max(b.chartWidth, b.chartHeight), l = this[(this.zoneAxis || "y") + "Axis"], q, t, p = b.inverted, x, A, F, y, v = !1; e.length && (m || n) && l && void 0 !== l.min && (t = l.reversed, x = l.horiz, m && m.hide(), n && n.hide(), q = l.getExtremes(), u(e, function (e, k) {
                    d = t ? x ? b.plotWidth : 0 : x ? 0 : l.toPixels(q.min); d = Math.min(Math.max(z(g, d), 0), r); g = Math.min(Math.max(Math.round(l.toPixels(z(e.value, q.max), !0)), 0), r); v && (d = g = l.toPixels(q.max)); A = Math.abs(d - g); F = Math.min(d, g); y = Math.max(d, g); l.isXAxis ? (h = {
                        x: p ? y : F, y: 0, width: A,
                        height: r
                    }, x || (h.x = b.plotHeight - h.x)) : (h = { x: 0, y: p ? y : F, width: r, height: A }, x && (h.y = b.plotWidth - h.y)); p && c.isVML && (h = l.isXAxis ? { x: 0, y: t ? F : y, height: h.width, width: b.chartWidth } : { x: h.y - b.plotLeft - b.spacingBox.x, y: 0, width: h.height, height: b.chartHeight }); f[k] ? f[k].animate(h) : (f[k] = c.clipRect(h), m && a["zone-graph-" + k].clip(f[k]), n && a["zone-area-" + k].clip(f[k])); v = e.value > q.max
                }), this.clips = f)
            }, invertGroups: function (a) {
                function b() {
                    var b = { width: c.yAxis.len, height: c.xAxis.len }; u(["group", "markerGroup"], function (e) {
                    c[e] &&
                        c[e].attr(b).invert(a)
                    })
                } var c = this, e; c.xAxis && (e = D(c.chart, "resize", b), D(c, "destroy", e), b(a), c.invertGroups = b)
            }, plotGroup: function (a, b, c, e, d) { var g = this[a], k = !g; k && (this[a] = g = this.chart.renderer.g(b).attr({ zIndex: e || .1 }).add(d), g.addClass("highcharts-series-" + this.index + " highcharts-" + this.type + "-series highcharts-color-" + this.colorIndex + " " + (this.options.className || ""))); g.attr({ visibility: c })[k ? "attr" : "animate"](this.getPlotBox()); return g }, getPlotBox: function () {
                var a = this.chart, b = this.xAxis, c =
                    this.yAxis; a.inverted && (b = c, c = this.xAxis); return { translateX: b ? b.left : a.plotLeft, translateY: c ? c.top : a.plotTop, scaleX: 1, scaleY: 1 }
            }, render: function () {
                var a = this, b = a.chart, c, e = a.options, d = !!a.animate && b.renderer.isSVG && C(e.animation).duration, g = a.visible ? "inherit" : "hidden", f = e.zIndex, h = a.hasRendered, m = b.seriesGroup, n = b.inverted; c = a.plotGroup("group", "series", g, f, m); a.markerGroup = a.plotGroup("markerGroup", "markers", g, f, m); d && a.animate(!0); c.inverted = a.isCartesian ? n : !1; a.drawGraph && (a.drawGraph(), a.applyZones());
                a.drawDataLabels && a.drawDataLabels(); a.visible && a.drawPoints(); a.drawTracker && !1 !== a.options.enableMouseTracking && a.drawTracker(); a.invertGroups(n); !1 === e.clip || a.sharedClipKey || h || c.clip(b.clipRect); d && a.animate(); h || (a.animationTimeout = x(function () { a.afterAnimate() }, d)); a.isDirty = a.isDirtyData = !1; a.hasRendered = !0
            }, redraw: function () {
                var a = this.chart, b = this.isDirty || this.isDirtyData, c = this.group, e = this.xAxis, d = this.yAxis; c && (a.inverted && c.attr({ width: a.plotWidth, height: a.plotHeight }), c.animate({
                    translateX: z(e &&
                        e.left, a.plotLeft), translateY: z(d && d.top, a.plotTop)
                })); this.translate(); this.render(); b && delete this.kdTree
            }, kdDimensions: 1, kdAxisArray: ["clientX", "plotY"], searchPoint: function (a, b) { var c = this.xAxis, e = this.yAxis, d = this.chart.inverted; return this.searchKDTree({ clientX: d ? c.len - a.chartY + c.pos : a.chartX - c.pos, plotY: d ? e.len - a.chartX + e.pos : a.chartY - e.pos }, b) }, buildKDTree: function () {
                function a(c, e, g) {
                    var d, k; if (k = c && c.length) return d = b.kdAxisArray[e % g], c.sort(function (a, b) { return a[d] - b[d] }), k = Math.floor(k /
                        2), { point: c[k], left: a(c.slice(0, k), e + 1, g), right: a(c.slice(k + 1), e + 1, g) }
                } var b = this, c = b.kdDimensions; delete b.kdTree; x(function () { b.kdTree = a(b.getValidPoints(null, !b.directTouch), c, c) }, b.options.kdNow ? 0 : 1)
            }, searchKDTree: function (a, b) {
                function c(a, b, f, h) {
                    var m = b.point, n = e.kdAxisArray[f % h], r, q, t = m; q = l(a[d]) && l(m[d]) ? Math.pow(a[d] - m[d], 2) : null; r = l(a[g]) && l(m[g]) ? Math.pow(a[g] - m[g], 2) : null; r = (q || 0) + (r || 0); m.dist = l(r) ? Math.sqrt(r) : Number.MAX_VALUE; m.distX = l(q) ? Math.sqrt(q) : Number.MAX_VALUE; n = a[n] - m[n]; r =
                        0 > n ? "left" : "right"; q = 0 > n ? "right" : "left"; b[r] && (r = c(a, b[r], f + 1, h), t = r[k] < t[k] ? r : m); b[q] && Math.sqrt(n * n) < t[k] && (a = c(a, b[q], f + 1, h), t = a[k] < t[k] ? a : t); return t
                } var e = this, d = this.kdAxisArray[0], g = this.kdAxisArray[1], k = b ? "distX" : "dist"; this.kdTree || this.buildKDTree(); if (this.kdTree) return c(a, this.kdTree, this.kdDimensions, this.kdDimensions)
            }
        })
    })(L); (function (a) {
        function D(a, d, c, f, h) {
            var n = a.chart.inverted; this.axis = a; this.isNegative = c; this.options = d; this.x = f; this.total = null; this.points = {}; this.stack = h; this.rightCliff =
                this.leftCliff = 0; this.alignOptions = { align: d.align || (n ? c ? "left" : "right" : "center"), verticalAlign: d.verticalAlign || (n ? "middle" : c ? "bottom" : "top"), y: l(d.y, n ? 4 : c ? 14 : -6), x: l(d.x, n ? c ? -6 : 6 : 0) }; this.textAlign = d.textAlign || (n ? c ? "right" : "left" : "center")
        } var C = a.Axis, G = a.Chart, I = a.correctFloat, h = a.defined, f = a.destroyObjectProperties, p = a.each, v = a.format, l = a.pick; a = a.Series; D.prototype = {
            destroy: function () { f(this, this.axis) }, render: function (a) {
                var d = this.options, c = d.format, c = c ? v(c, this) : d.formatter.call(this); this.label ?
                    this.label.attr({ text: c, visibility: "hidden" }) : this.label = this.axis.chart.renderer.text(c, null, null, d.useHTML).css(d.style).attr({ align: this.textAlign, rotation: d.rotation, visibility: "hidden" }).add(a)
            }, setOffset: function (a, d) {
                var c = this.axis, f = c.chart, h = f.inverted, l = c.reversed, l = this.isNegative && !l || !this.isNegative && l, m = c.translate(c.usePercentage ? 100 : this.total, 0, 0, 0, 1), c = c.translate(0), c = Math.abs(m - c); a = f.xAxis[0].translate(this.x) + a; var b = f.plotHeight, h = {
                    x: h ? l ? m : m - c : a, y: h ? b - a - d : l ? b - m - c : b - m, width: h ?
                        c : d, height: h ? d : c
                }; if (d = this.label) d.align(this.alignOptions, null, h), h = d.alignAttr, d[!1 === this.options.crop || f.isInsidePlot(h.x, h.y) ? "show" : "hide"](!0)
            }
        }; G.prototype.getStacks = function () { var a = this; p(a.yAxis, function (a) { a.stacks && a.hasVisibleSeries && (a.oldStacks = a.stacks) }); p(a.series, function (d) { !d.options.stacking || !0 !== d.visible && !1 !== a.options.chart.ignoreHiddenSeries || (d.stackKey = d.type + l(d.options.stack, "")) }) }; C.prototype.buildStacks = function () {
            var a = this.series, d, c = l(this.options.reversedStacks,
                !0), f = a.length, h; if (!this.isXAxis) { this.usePercentage = !1; for (h = f; h--;)a[c ? h : f - h - 1].setStackedPoints(); for (h = f; h--;)d = a[c ? h : f - h - 1], d.setStackCliffs && d.setStackCliffs(); if (this.usePercentage) for (h = 0; h < f; h++)a[h].setPercentStacks() }
        }; C.prototype.renderStackTotals = function () { var a = this.chart, d = a.renderer, c = this.stacks, f, h, l = this.stackTotalGroup; l || (this.stackTotalGroup = l = d.g("stack-labels").attr({ visibility: "visible", zIndex: 6 }).add()); l.translate(a.plotLeft, a.plotTop); for (f in c) for (h in a = c[f], a) a[h].render(l) };
        C.prototype.resetStacks = function () { var a = this.stacks, d, c; if (!this.isXAxis) for (d in a) for (c in a[d]) a[d][c].touched < this.stacksTouched ? (a[d][c].destroy(), delete a[d][c]) : (a[d][c].total = null, a[d][c].cum = null) }; C.prototype.cleanStacks = function () { var a, d, c; if (!this.isXAxis) for (d in this.oldStacks && (a = this.stacks = this.oldStacks), a) for (c in a[d]) a[d][c].cum = a[d][c].total }; a.prototype.setStackedPoints = function () {
            if (this.options.stacking && (!0 === this.visible || !1 === this.chart.options.chart.ignoreHiddenSeries)) {
                var a =
                    this.processedXData, d = this.processedYData, c = [], f = d.length, p = this.options, t = p.threshold, m = p.startFromThreshold ? t : 0, b = p.stack, p = p.stacking, q = this.stackKey, v = "-" + q, F = this.negStacks, e = this.yAxis, r = e.stacks, x = e.oldStacks, A, k, w, C, J, G, g; e.stacksTouched += 1; for (J = 0; J < f; J++)G = a[J], g = d[J], A = this.getStackIndicator(A, G, this.index), C = A.key, w = (k = F && g < (m ? 0 : t)) ? v : q, r[w] || (r[w] = {}), r[w][G] || (x[w] && x[w][G] ? (r[w][G] = x[w][G], r[w][G].total = null) : r[w][G] = new D(e, e.options.stackLabels, k, G, b)), w = r[w][G], null !== g && (w.points[C] =
                        w.points[this.index] = [l(w.cum, m)], h(w.cum) || (w.base = C), w.touched = e.stacksTouched, 0 < A.index && !1 === this.singleStacks && (w.points[C][0] = w.points[this.index + "," + G + ",0"][0])), "percent" === p ? (k = k ? q : v, F && r[k] && r[k][G] ? (k = r[k][G], w.total = k.total = Math.max(k.total, w.total) + Math.abs(g) || 0) : w.total = I(w.total + (Math.abs(g) || 0))) : w.total = I(w.total + (g || 0)), w.cum = l(w.cum, m) + (g || 0), null !== g && (w.points[C].push(w.cum), c[J] = w.cum); "percent" === p && (e.usePercentage = !0); this.stackedYData = c; e.oldStacks = {}
            }
        }; a.prototype.setPercentStacks =
            function () { var a = this, d = a.stackKey, c = a.yAxis.stacks, f = a.processedXData, h; p([d, "-" + d], function (d) { for (var m = f.length, b, n; m--;)if (b = f[m], h = a.getStackIndicator(h, b, a.index, d), b = (n = c[d] && c[d][b]) && n.points[h.key]) n = n.total ? 100 / n.total : 0, b[0] = I(b[0] * n), b[1] = I(b[1] * n), a.stackedYData[m] = b[1] }) }; a.prototype.getStackIndicator = function (a, d, c, f) { !h(a) || a.x !== d || f && a.key !== f ? a = { x: d, index: 0, key: f } : a.index++; a.key = [c, d, a.index].join(); return a }
    })(L); (function (a) {
        var D = a.addEvent, C = a.animate, G = a.Axis, I = a.createElement,
        h = a.css, f = a.defined, p = a.each, v = a.erase, l = a.extend, u = a.fireEvent, d = a.inArray, c = a.isNumber, n = a.isObject, y = a.merge, t = a.pick, m = a.Point, b = a.Series, q = a.seriesTypes, z = a.setAnimation, F = a.splat; l(a.Chart.prototype, {
            addSeries: function (a, b, c) { var e, d = this; a && (b = t(b, !0), u(d, "addSeries", { options: a }, function () { e = d.initSeries(a); d.isDirtyLegend = !0; d.linkSeries(); b && d.redraw(c) })); return e }, addAxis: function (a, b, c, d) {
                var e = b ? "xAxis" : "yAxis", f = this.options; a = y(a, { index: this[e].length, isX: b }); new G(this, a); f[e] = F(f[e] ||
                    {}); f[e].push(a); t(c, !0) && this.redraw(d)
            }, showLoading: function (a) {
                var b = this, c = b.options, e = b.loadingDiv, d = c.loading, f = function () { e && h(e, { left: b.plotLeft + "px", top: b.plotTop + "px", width: b.plotWidth + "px", height: b.plotHeight + "px" }) }; e || (b.loadingDiv = e = I("div", { className: "highcharts-loading highcharts-loading-hidden" }, null, b.container), b.loadingSpan = I("span", { className: "highcharts-loading-inner" }, null, e), D(b, "redraw", f)); e.className = "highcharts-loading"; b.loadingSpan.innerHTML = a || c.lang.loading; h(e, l(d.style,
                    { zIndex: 10 })); h(b.loadingSpan, d.labelStyle); b.loadingShown || (h(e, { opacity: 0, display: "" }), C(e, { opacity: d.style.opacity || .5 }, { duration: d.showDuration || 0 })); b.loadingShown = !0; f()
            }, hideLoading: function () { var a = this.options, b = this.loadingDiv; b && (b.className = "highcharts-loading highcharts-loading-hidden", C(b, { opacity: 0 }, { duration: a.loading.hideDuration || 100, complete: function () { h(b, { display: "none" }) } })); this.loadingShown = !1 }, propsRequireDirtyBox: "backgroundColor borderColor borderWidth margin marginTop marginRight marginBottom marginLeft spacing spacingTop spacingRight spacingBottom spacingLeft borderRadius plotBackgroundColor plotBackgroundImage plotBorderColor plotBorderWidth plotShadow shadow".split(" "),
            propsRequireUpdateSeries: "chart.inverted chart.polar chart.ignoreHiddenSeries chart.type colors plotOptions".split(" "), update: function (a, b) {
                var e, h = { credits: "addCredits", title: "setTitle", subtitle: "setSubtitle" }, k = a.chart, m, n; if (k) {
                    y(!0, this.options.chart, k); "className" in k && this.setClassName(k.className); if ("inverted" in k || "polar" in k) this.propFromSeries(), m = !0; for (e in k) k.hasOwnProperty(e) && (-1 !== d("chart." + e, this.propsRequireUpdateSeries) && (n = !0), -1 !== d(e, this.propsRequireDirtyBox) && (this.isDirtyBox =
                        !0)); "style" in k && this.renderer.setStyle(k.style)
                } for (e in a) { if (this[e] && "function" === typeof this[e].update) this[e].update(a[e], !1); else if ("function" === typeof this[h[e]]) this[h[e]](a[e]); "chart" !== e && -1 !== d(e, this.propsRequireUpdateSeries) && (n = !0) } a.colors && (this.options.colors = a.colors); a.plotOptions && y(!0, this.options.plotOptions, a.plotOptions); p(["xAxis", "yAxis", "series"], function (b) { a[b] && p(F(a[b]), function (a) { var c = f(a.id) && this.get(a.id) || this[b][0]; c && c.coll === b && c.update(a, !1) }, this) }, this);
                m && p(this.axes, function (a) { a.update({}, !1) }); n && p(this.series, function (a) { a.update({}, !1) }); a.loading && y(!0, this.options.loading, a.loading); e = k && k.width; k = k && k.height; c(e) && e !== this.chartWidth || c(k) && k !== this.chartHeight ? this.setSize(e, k) : t(b, !0) && this.redraw()
            }, setSubtitle: function (a) { this.setTitle(void 0, a) }
        }); l(m.prototype, {
            update: function (a, b, c, d) {
                function e() {
                    f.applyOptions(a); null === f.y && m && (f.graphic = m.destroy()); n(a, !0) && (m && m.element && a && a.marker && a.marker.symbol && (f.graphic = m.destroy()),
                        a && a.dataLabels && f.dataLabel && (f.dataLabel = f.dataLabel.destroy())); l = f.index; h.updateParallelArrays(f, l); r.data[l] = n(r.data[l], !0) ? f.options : a; h.isDirty = h.isDirtyData = !0; !h.fixedBox && h.hasCartesianSeries && (g.isDirtyBox = !0); "point" === r.legendType && (g.isDirtyLegend = !0); b && g.redraw(c)
                } var f = this, h = f.series, m = f.graphic, l, g = h.chart, r = h.options; b = t(b, !0); !1 === d ? e() : f.firePointEvent("update", { options: a }, e)
            }, remove: function (a, b) { this.series.removePoint(d(this, this.series.data), a, b) }
        }); l(b.prototype, {
            addPoint: function (a,
                b, c, d) {
                    var e = this.options, f = this.data, h = this.chart, m = this.xAxis && this.xAxis.names, n = e.data, g, l, r = this.xData, q, p; b = t(b, !0); g = { series: this }; this.pointClass.prototype.applyOptions.apply(g, [a]); p = g.x; q = r.length; if (this.requireSorting && p < r[q - 1]) for (l = !0; q && r[q - 1] > p;)q--; this.updateParallelArrays(g, "splice", q, 0, 0); this.updateParallelArrays(g, q); m && g.name && (m[p] = g.name); n.splice(q, 0, a); l && (this.data.splice(q, 0, null), this.processData()); "point" === e.legendType && this.generatePoints(); c && (f[0] && f[0].remove ?
                        f[0].remove(!1) : (f.shift(), this.updateParallelArrays(g, "shift"), n.shift())); this.isDirtyData = this.isDirty = !0; b && h.redraw(d)
            }, removePoint: function (a, b, c) { var e = this, d = e.data, f = d[a], h = e.points, m = e.chart, n = function () { h && h.length === d.length && h.splice(a, 1); d.splice(a, 1); e.options.data.splice(a, 1); e.updateParallelArrays(f || { series: e }, "splice", a, 1); f && f.destroy(); e.isDirty = !0; e.isDirtyData = !0; b && m.redraw() }; z(c, m); b = t(b, !0); f ? f.firePointEvent("remove", null, n) : n() }, remove: function (a, b, c) {
                function e() {
                    d.destroy();
                    f.isDirtyLegend = f.isDirtyBox = !0; f.linkSeries(); t(a, !0) && f.redraw(b)
                } var d = this, f = d.chart; !1 !== c ? u(d, "remove", null, e) : e()
            }, update: function (a, b) {
                var c = this, e = this.chart, d = this.userOptions, f = this.type, h = a.type || d.type || e.options.chart.type, m = q[f].prototype, n = ["group", "markerGroup", "dataLabelsGroup"], g; if (h && h !== f || void 0 !== a.zIndex) n.length = 0; p(n, function (a) { n[a] = c[a]; delete c[a] }); a = y(d, { animation: !1, index: this.index, pointStart: this.xData[0] }, { data: this.options.data }, a); this.remove(!1, null, !1); for (g in m) this[g] =
                    void 0; l(this, q[h || f].prototype); p(n, function (a) { c[a] = n[a] }); this.init(e, a); e.linkSeries(); t(b, !0) && e.redraw(!1)
            }
        }); l(G.prototype, {
            update: function (a, b) { var c = this.chart; a = c.options[this.coll][this.options.index] = y(this.userOptions, a); this.destroy(!0); this.init(c, l(a, { events: void 0 })); c.isDirtyBox = !0; t(b, !0) && c.redraw() }, remove: function (a) {
                for (var b = this.chart, c = this.coll, e = this.series, d = e.length; d--;)e[d] && e[d].remove(!1); v(b.axes, this); v(b[c], this); b.options[c].splice(this.options.index, 1); p(b[c],
                    function (a, b) { a.options.index = b }); this.destroy(); b.isDirtyBox = !0; t(a, !0) && b.redraw()
            }, setTitle: function (a, b) { this.update({ title: a }, b) }, setCategories: function (a, b) { this.update({ categories: a }, b) }
        })
    })(L); (function (a) {
        var D = a.color, C = a.each, G = a.map, I = a.pick, h = a.Series, f = a.seriesType; f("area", "line", { softThreshold: !1, threshold: 0 }, {
            singleStacks: !1, getStackPoints: function () {
                var a = [], f = [], h = this.xAxis, u = this.yAxis, d = u.stacks[this.stackKey], c = {}, n = this.points, y = this.index, t = u.series, m = t.length, b, q = I(u.options.reversedStacks,
                    !0) ? 1 : -1, z, F; if (this.options.stacking) {
                        for (z = 0; z < n.length; z++)c[n[z].x] = n[z]; for (F in d) null !== d[F].total && f.push(F); f.sort(function (a, b) { return a - b }); b = G(t, function () { return this.visible }); C(f, function (e, n) {
                            var l = 0, r, k; if (c[e] && !c[e].isNull) a.push(c[e]), C([-1, 1], function (a) { var h = 1 === a ? "rightNull" : "leftNull", l = 0, t = d[f[n + a]]; if (t) for (z = y; 0 <= z && z < m;)r = t.points[z], r || (z === y ? c[e][h] = !0 : b[z] && (k = d[e].points[z]) && (l -= k[1] - k[0])), z += q; c[e][1 === a ? "rightCliff" : "leftCliff"] = l }); else {
                                for (z = y; 0 <= z && z < m;) {
                                    if (r =
                                        d[e].points[z]) { l = r[1]; break } z += q
                                } l = u.toPixels(l, !0); a.push({ isNull: !0, plotX: h.toPixels(e, !0), plotY: l, yBottom: l })
                            }
                        })
                    } return a
            }, getGraphPath: function (a) {
                var f = h.prototype.getGraphPath, l = this.options, p = l.stacking, d = this.yAxis, c, n, y = [], t = [], m = this.index, b, q = d.stacks[this.stackKey], z = l.threshold, F = d.getThreshold(l.threshold), e, l = l.connectNulls || "percent" === p, r = function (c, e, f) {
                    var k = a[c]; c = p && q[k.x].points[m]; var h = k[f + "Null"] || 0; f = k[f + "Cliff"] || 0; var n, l, k = !0; f || h ? (n = (h ? c[0] : c[1]) + f, l = c[0] + f, k = !!h) : !p &&
                        a[e] && a[e].isNull && (n = l = z); void 0 !== n && (t.push({ plotX: b, plotY: null === n ? F : d.getThreshold(n), isNull: k }), y.push({ plotX: b, plotY: null === l ? F : d.getThreshold(l), doCurve: !1 }))
                }; a = a || this.points; p && (a = this.getStackPoints()); for (c = 0; c < a.length; c++)if (n = a[c].isNull, b = I(a[c].rectPlotX, a[c].plotX), e = I(a[c].yBottom, F), !n || l) l || r(c, c - 1, "left"), n && !p && l || (t.push(a[c]), y.push({ x: c, plotX: b, plotY: e })), l || r(c, c + 1, "right"); c = f.call(this, t, !0, !0); y.reversed = !0; n = f.call(this, y, !0, !0); n.length && (n[0] = "L"); n = c.concat(n); f =
                    f.call(this, t, !1, l); n.xMap = c.xMap; this.areaPath = n; return f
            }, drawGraph: function () {
            this.areaPath = []; h.prototype.drawGraph.apply(this); var a = this, f = this.areaPath, l = this.options, u = [["area", "highcharts-area", this.color, l.fillColor]]; C(this.zones, function (d, c) { u.push(["zone-area-" + c, "highcharts-area highcharts-zone-area-" + c + " " + d.className, d.color || a.color, d.fillColor || l.fillColor]) }); C(u, function (d) {
                var c = d[0], h = a[c]; h ? (h.endX = f.xMap, h.animate({ d: f })) : (h = a[c] = a.chart.renderer.path(f).addClass(d[1]).attr({
                    fill: I(d[3],
                        D(d[2]).setOpacity(I(l.fillOpacity, .75)).get()), zIndex: 0
                }).add(a.group), h.isArea = !0); h.startX = f.xMap; h.shiftUnit = l.step ? 2 : 1
            })
            }, drawLegendSymbol: a.LegendSymbolMixin.drawRectangle
        })
    })(L); (function (a) {
        var D = a.pick; a = a.seriesType; a("spline", "line", {}, {
            getPointSpline: function (a, G, I) {
                var h = G.plotX, f = G.plotY, p = a[I - 1]; I = a[I + 1]; var v, l, u, d; if (p && !p.isNull && !1 !== p.doCurve && I && !I.isNull && !1 !== I.doCurve) {
                    a = p.plotY; u = I.plotX; I = I.plotY; var c = 0; v = (1.5 * h + p.plotX) / 2.5; l = (1.5 * f + a) / 2.5; u = (1.5 * h + u) / 2.5; d = (1.5 * f + I) / 2.5;
                    u !== v && (c = (d - l) * (u - h) / (u - v) + f - d); l += c; d += c; l > a && l > f ? (l = Math.max(a, f), d = 2 * f - l) : l < a && l < f && (l = Math.min(a, f), d = 2 * f - l); d > I && d > f ? (d = Math.max(I, f), l = 2 * f - d) : d < I && d < f && (d = Math.min(I, f), l = 2 * f - d); G.rightContX = u; G.rightContY = d
                } G = ["C", D(p.rightContX, p.plotX), D(p.rightContY, p.plotY), D(v, h), D(l, f), h, f]; p.rightContX = p.rightContY = null; return G
            }
        })
    })(L); (function (a) {
        var D = a.seriesTypes.area.prototype, C = a.seriesType; C("areaspline", "spline", a.defaultPlotOptions.area, {
            getStackPoints: D.getStackPoints, getGraphPath: D.getGraphPath,
            setStackCliffs: D.setStackCliffs, drawGraph: D.drawGraph, drawLegendSymbol: a.LegendSymbolMixin.drawRectangle
        })
    })(L); (function (a) {
        var D = a.animObject, C = a.color, G = a.each, I = a.extend, h = a.isNumber, f = a.merge, p = a.pick, v = a.Series, l = a.seriesType, u = a.svg; l("column", "line", {
            borderRadius: 0, groupPadding: .2, marker: null, pointPadding: .1, minPointLength: 0, cropThreshold: 50, pointRange: null, states: { hover: { halo: !1, brightness: .1, shadow: !1 }, select: { color: "#cccccc", borderColor: "#000000", shadow: !1 } }, dataLabels: {
                align: null, verticalAlign: null,
                y: null
            }, softThreshold: !1, startFromThreshold: !0, stickyTracking: !1, tooltip: { distance: 6 }, threshold: 0, borderColor: "#ffffff"
        }, {
            cropShoulder: 0, directTouch: !0, trackerGroups: ["group", "dataLabelsGroup"], negStacks: !0, init: function () { v.prototype.init.apply(this, arguments); var a = this, c = a.chart; c.hasRendered && G(c.series, function (c) { c.type === a.type && (c.isDirty = !0) }) }, getColumnMetrics: function () {
                var a = this, c = a.options, f = a.xAxis, h = a.yAxis, l = f.reversed, m, b = {}, q = 0; !1 === c.grouping ? q = 1 : G(a.chart.series, function (c) {
                    var e =
                        c.options, d = c.yAxis, f; c.type === a.type && c.visible && h.len === d.len && h.pos === d.pos && (e.stacking ? (m = c.stackKey, void 0 === b[m] && (b[m] = q++), f = b[m]) : !1 !== e.grouping && (f = q++), c.columnIndex = f)
                }); var u = Math.min(Math.abs(f.transA) * (f.ordinalSlope || c.pointRange || f.closestPointRange || f.tickInterval || 1), f.len), F = u * c.groupPadding, e = (u - 2 * F) / q, c = Math.min(c.maxPointWidth || f.len, p(c.pointWidth, e * (1 - 2 * c.pointPadding))); a.columnMetrics = { width: c, offset: (e - c) / 2 + (F + ((a.columnIndex || 0) + (l ? 1 : 0)) * e - u / 2) * (l ? -1 : 1) }; return a.columnMetrics
            },
            crispCol: function (a, c, f, h) { var d = this.chart, m = this.borderWidth, b = -(m % 2 ? .5 : 0), m = m % 2 ? .5 : 1; d.inverted && d.renderer.isVML && (m += 1); f = Math.round(a + f) + b; a = Math.round(a) + b; h = Math.round(c + h) + m; b = .5 >= Math.abs(c) && .5 < h; c = Math.round(c) + m; h -= c; b && h && (--c, h += 1); return { x: a, y: c, width: f - a, height: h } }, translate: function () {
                var a = this, c = a.chart, f = a.options, h = a.dense = 2 > a.closestPointRange * a.xAxis.transA, h = a.borderWidth = p(f.borderWidth, h ? 0 : 1), l = a.yAxis, m = a.translatedThreshold = l.getThreshold(f.threshold), b = p(f.minPointLength,
                    5), q = a.getColumnMetrics(), u = q.width, F = a.barW = Math.max(u, 1 + 2 * h), e = a.pointXOffset = q.offset; c.inverted && (m -= .5); f.pointPadding && (F = Math.ceil(F)); v.prototype.translate.apply(a); G(a.points, function (d) {
                        var f = p(d.yBottom, m), h = 999 + Math.abs(f), h = Math.min(Math.max(-h, d.plotY), l.len + h), k = d.plotX + e, n = F, q = Math.min(h, f), r, t = Math.max(h, f) - q; Math.abs(t) < b && b && (t = b, r = !l.reversed && !d.negative || l.reversed && d.negative, q = Math.abs(q - m) > b ? f - b : m - (r ? b : 0)); d.barX = k; d.pointWidth = u; d.tooltipPos = c.inverted ? [l.len + l.pos - c.plotLeft -
                            h, a.xAxis.len - k - n / 2, t] : [k + n / 2, h + l.pos - c.plotTop, t]; d.shapeType = "rect"; d.shapeArgs = a.crispCol.apply(a, d.isNull ? [d.plotX, l.len / 2, 0, 0] : [k, q, n, t])
                    })
            }, getSymbol: a.noop, drawLegendSymbol: a.LegendSymbolMixin.drawRectangle, drawGraph: function () { this.group[this.dense ? "addClass" : "removeClass"]("highcharts-dense-data") }, pointAttribs: function (a, c) {
                var d = this.options, f, h = this.pointAttrToOptions || {}; f = h.stroke || "borderColor"; var m = h["stroke-width"] || "borderWidth", b = a && a.color || this.color, l = a[f] || d[f] || this.color ||
                    b, p = a[m] || d[m] || this[m] || 0, h = d.dashStyle; a && this.zones.length && (b = (b = a.getZone()) && b.color || a.options.color || this.color); c && (a = d.states[c], c = a.brightness, b = a.color || void 0 !== c && C(b).brighten(a.brightness).get() || b, l = a[f] || l, p = a[m] || p, h = a.dashStyle || h); f = { fill: b, stroke: l, "stroke-width": p }; d.borderRadius && (f.r = d.borderRadius); h && (f.dashstyle = h); return f
            }, drawPoints: function () {
                var a = this, c = this.chart, l = a.options, p = c.renderer, t = l.animationLimit || 250, m; G(a.points, function (b) {
                    var d = b.graphic; if (h(b.plotY) &&
                        null !== b.y) { m = b.shapeArgs; if (d) d[c.pointCount < t ? "animate" : "attr"](f(m)); else b.graphic = d = p[b.shapeType](m).attr({ "class": b.getClassName() }).add(b.group || a.group); d.attr(a.pointAttribs(b, b.selected && "select")).shadow(l.shadow, null, l.stacking && !l.borderRadius) } else d && (b.graphic = d.destroy())
                })
            }, animate: function (a) {
                var c = this, d = this.yAxis, f = c.options, h = this.chart.inverted, m = {}; u && (a ? (m.scaleY = .001, a = Math.min(d.pos + d.len, Math.max(d.pos, d.toPixels(f.threshold))), h ? m.translateX = a - d.len : m.translateY = a, c.group.attr(m)) :
                    (m[h ? "translateX" : "translateY"] = d.pos, c.group.animate(m, I(D(c.options.animation), { step: function (a, d) { c.group.attr({ scaleY: Math.max(.001, d.pos) }) } })), c.animate = null))
            }, remove: function () { var a = this, c = a.chart; c.hasRendered && G(c.series, function (c) { c.type === a.type && (c.isDirty = !0) }); v.prototype.remove.apply(a, arguments) }
        })
    })(L); (function (a) { a = a.seriesType; a("bar", "column", null, { inverted: !0 }) })(L); (function (a) {
        var D = a.Series; a = a.seriesType; a("scatter", "line", {
            lineWidth: 0, marker: { enabled: !0 }, tooltip: {
                headerFormat: '\x3cspan style\x3d"color:{point.color}"\x3e\u25cf\x3c/span\x3e \x3cspan style\x3d"font-size: 0.85em"\x3e {series.name}\x3c/span\x3e\x3cbr/\x3e',
                pointFormat: "x: \x3cb\x3e{point.x}\x3c/b\x3e\x3cbr/\x3ey: \x3cb\x3e{point.y}\x3c/b\x3e\x3cbr/\x3e"
            }
        }, { sorted: !1, requireSorting: !1, noSharedTooltip: !0, trackerGroups: ["group", "markerGroup", "dataLabelsGroup"], takeOrdinalPosition: !1, kdDimensions: 2, drawGraph: function () { this.options.lineWidth && D.prototype.drawGraph.call(this) } })
    })(L); (function (a) {
        var D = a.pick, C = a.relativeLength; a.CenteredSeriesMixin = {
            getCenter: function () {
                var a = this.options, I = this.chart, h = 2 * (a.slicedOffset || 0), f = I.plotWidth - 2 * h, I = I.plotHeight -
                    2 * h, p = a.center, p = [D(p[0], "50%"), D(p[1], "50%"), a.size || "100%", a.innerSize || 0], v = Math.min(f, I), l, u; for (l = 0; 4 > l; ++l)u = p[l], a = 2 > l || 2 === l && /%$/.test(u), p[l] = C(u, [f, I, v, p[2]][l]) + (a ? h : 0); p[3] > p[2] && (p[3] = p[2]); return p
            }
        }
    })(L); (function (a) {
        var D = a.addEvent, C = a.defined, G = a.each, I = a.extend, h = a.inArray, f = a.noop, p = a.pick, v = a.Point, l = a.Series, u = a.seriesType, d = a.setAnimation; u("pie", "line", {
            center: [null, null], clip: !1, colorByPoint: !0, dataLabels: {
                distance: 30, enabled: !0, formatter: function () {
                    return null === this.y ?
                        void 0 : this.point.name
                }, x: 0
            }, ignoreHiddenPoint: !0, legendType: "point", marker: null, size: null, showInLegend: !1, slicedOffset: 10, stickyTracking: !1, tooltip: { followPointer: !0 }, borderColor: "#ffffff", borderWidth: 1, states: { hover: { brightness: .1, shadow: !1 } }
        }, {
            isCartesian: !1, requireSorting: !1, directTouch: !0, noSharedTooltip: !0, trackerGroups: ["group", "dataLabelsGroup"], axisTypes: [], pointAttribs: a.seriesTypes.column.prototype.pointAttribs, animate: function (a) {
                var c = this, d = c.points, f = c.startAngleRad; a || (G(d, function (a) {
                    var b =
                        a.graphic, d = a.shapeArgs; b && (b.attr({ r: a.startR || c.center[3] / 2, start: f, end: f }), b.animate({ r: d.r, start: d.start, end: d.end }, c.options.animation))
                }), c.animate = null)
            }, updateTotals: function () { var a, d = 0, f = this.points, h = f.length, m, b = this.options.ignoreHiddenPoint; for (a = 0; a < h; a++)m = f[a], 0 > m.y && (m.y = null), d += b && !m.visible ? 0 : m.y; this.total = d; for (a = 0; a < h; a++)m = f[a], m.percentage = 0 < d && (m.visible || !b) ? m.y / d * 100 : 0, m.total = d }, generatePoints: function () { l.prototype.generatePoints.call(this); this.updateTotals() }, translate: function (a) {
                this.generatePoints();
                var c = 0, d = this.options, f = d.slicedOffset, h = f + (d.borderWidth || 0), b, l, u, F = d.startAngle || 0, e = this.startAngleRad = Math.PI / 180 * (F - 90), F = (this.endAngleRad = Math.PI / 180 * (p(d.endAngle, F + 360) - 90)) - e, r = this.points, x = d.dataLabels.distance, d = d.ignoreHiddenPoint, A, k = r.length, w; a || (this.center = a = this.getCenter()); this.getX = function (b, c) { u = Math.asin(Math.min((b - a[1]) / (a[2] / 2 + x), 1)); return a[0] + (c ? -1 : 1) * Math.cos(u) * (a[2] / 2 + x) }; for (A = 0; A < k; A++) {
                    w = r[A]; b = e + c * F; if (!d || w.visible) c += w.percentage / 100; l = e + c * F; w.shapeType =
                        "arc"; w.shapeArgs = { x: a[0], y: a[1], r: a[2] / 2, innerR: a[3] / 2, start: Math.round(1E3 * b) / 1E3, end: Math.round(1E3 * l) / 1E3 }; u = (l + b) / 2; u > 1.5 * Math.PI ? u -= 2 * Math.PI : u < -Math.PI / 2 && (u += 2 * Math.PI); w.slicedTranslation = { translateX: Math.round(Math.cos(u) * f), translateY: Math.round(Math.sin(u) * f) }; b = Math.cos(u) * a[2] / 2; l = Math.sin(u) * a[2] / 2; w.tooltipPos = [a[0] + .7 * b, a[1] + .7 * l]; w.half = u < -Math.PI / 2 || u > Math.PI / 2 ? 1 : 0; w.angle = u; h = Math.min(h, x / 5); w.labelPos = [a[0] + b + Math.cos(u) * x, a[1] + l + Math.sin(u) * x, a[0] + b + Math.cos(u) * h, a[1] + l + Math.sin(u) *
                            h, a[0] + b, a[1] + l, 0 > x ? "center" : w.half ? "right" : "left", u]
                }
            }, drawGraph: null, drawPoints: function () {
                var a = this, d = a.chart.renderer, f, h, m, b, l = a.options.shadow; l && !a.shadowGroup && (a.shadowGroup = d.g("shadow").add(a.group)); G(a.points, function (c) {
                    if (null !== c.y) {
                        h = c.graphic; b = c.shapeArgs; f = c.sliced ? c.slicedTranslation : {}; var n = c.shadowGroup; l && !n && (n = c.shadowGroup = d.g("shadow").add(a.shadowGroup)); n && n.attr(f); m = a.pointAttribs(c, c.selected && "select"); h ? h.setRadialReference(a.center).attr(m).animate(I(b, f)) : (c.graphic =
                            h = d[c.shapeType](b).addClass(c.getClassName()).setRadialReference(a.center).attr(f).add(a.group), c.visible || h.attr({ visibility: "hidden" }), h.attr(m).attr({ "stroke-linejoin": "round" }).shadow(l, n))
                    }
                })
            }, searchPoint: f, sortByAngle: function (a, d) { a.sort(function (a, c) { return void 0 !== a.angle && (c.angle - a.angle) * d }) }, drawLegendSymbol: a.LegendSymbolMixin.drawRectangle, getCenter: a.CenteredSeriesMixin.getCenter, getSymbol: f
        }, {
            init: function () {
                v.prototype.init.apply(this, arguments); var a = this, d; a.name = p(a.name, "Slice");
                d = function (c) { a.slice("select" === c.type) }; D(a, "select", d); D(a, "unselect", d); return a
            }, setVisible: function (a, d) { var c = this, f = c.series, m = f.chart, b = f.options.ignoreHiddenPoint; d = p(d, b); a !== c.visible && (c.visible = c.options.visible = a = void 0 === a ? !c.visible : a, f.options.data[h(c, f.data)] = c.options, G(["graphic", "dataLabel", "connector", "shadowGroup"], function (b) { if (c[b]) c[b][a ? "show" : "hide"](!0) }), c.legendItem && m.legend.colorizeItem(c, a), a || "hover" !== c.state || c.setState(""), b && (f.isDirty = !0), d && m.redraw()) },
            slice: function (a, f, l) { var c = this.series; d(l, c.chart); p(f, !0); this.sliced = this.options.sliced = a = C(a) ? a : !this.sliced; c.options.data[h(this, c.data)] = this.options; a = a ? this.slicedTranslation : { translateX: 0, translateY: 0 }; this.graphic.animate(a); this.shadowGroup && this.shadowGroup.animate(a) }, haloPath: function (a) { var c = this.shapeArgs; return this.sliced || !this.visible ? [] : this.series.chart.renderer.symbols.arc(c.x, c.y, c.r + a, c.r + a, { innerR: this.shapeArgs.r, start: c.start, end: c.end }) }
        })
    })(L); (function (a) {
        var D =
            a.addEvent, C = a.arrayMax, G = a.defined, I = a.each, h = a.extend, f = a.format, p = a.map, v = a.merge, l = a.noop, u = a.pick, d = a.relativeLength, c = a.Series, n = a.seriesTypes, y = a.stableSort; a.distribute = function (a, c) {
                function b(a, b) { return a.target - b.target } var d, f = !0, h = a, e = [], m; m = 0; for (d = a.length; d--;)m += a[d].size; if (m > c) { y(a, function (a, b) { return (b.rank || 0) - (a.rank || 0) }); for (m = d = 0; m <= c;)m += a[d].size, d++; e = a.splice(d - 1, a.length) } y(a, b); for (a = p(a, function (a) { return { size: a.size, targets: [a.target] } }); f;) {
                    for (d = a.length; d--;)f =
                        a[d], m = (Math.min.apply(0, f.targets) + Math.max.apply(0, f.targets)) / 2, f.pos = Math.min(Math.max(0, m - f.size / 2), c - f.size); d = a.length; for (f = !1; d--;)0 < d && a[d - 1].pos + a[d - 1].size > a[d].pos && (a[d - 1].size += a[d].size, a[d - 1].targets = a[d - 1].targets.concat(a[d].targets), a[d - 1].pos + a[d - 1].size > c && (a[d - 1].pos = c - a[d - 1].size), a.splice(d, 1), f = !0)
                } d = 0; I(a, function (a) { var b = 0; I(a.targets, function () { h[d].pos = a.pos + b; b += h[d].size; d++ }) }); h.push.apply(h, e); y(h, b)
            }; c.prototype.drawDataLabels = function () {
                var a = this, c = a.options,
                b = c.dataLabels, d = a.points, l, n, e = a.hasRendered || 0, r, p, A = u(b.defer, !0), k = a.chart.renderer; if (b.enabled || a._hasPointLabels) a.dlProcessOptions && a.dlProcessOptions(b), p = a.plotGroup("dataLabelsGroup", "data-labels", A && !e ? "hidden" : "visible", b.zIndex || 6), A && (p.attr({ opacity: +e }), e || D(a, "afterAnimate", function () { a.visible && p.show(!0); p[c.animation ? "animate" : "attr"]({ opacity: 1 }, { duration: 200 }) })), n = b, I(d, function (e) {
                    var d, m = e.dataLabel, q, g, t = e.connector, F = !0, x, A = {}; l = e.dlOptions || e.options && e.options.dataLabels;
                    d = u(l && l.enabled, n.enabled) && null !== e.y; if (m && !d) e.dataLabel = m.destroy(); else if (d) {
                        b = v(n, l); x = b.style; d = b.rotation; q = e.getLabelConfig(); r = b.format ? f(b.format, q) : b.formatter.call(q, b); x.color = u(b.color, x.color, a.color, "#000000"); if (m) G(r) ? (m.attr({ text: r }), F = !1) : (e.dataLabel = m = m.destroy(), t && (e.connector = t.destroy())); else if (G(r)) {
                            m = { fill: b.backgroundColor, stroke: b.borderColor, "stroke-width": b.borderWidth, r: b.borderRadius || 0, rotation: d, padding: b.padding, zIndex: 1 }; "contrast" === x.color && (A.color = b.inside ||
                                0 > b.distance || c.stacking ? k.getContrast(e.color || a.color) : "#000000"); c.cursor && (A.cursor = c.cursor); for (g in m) void 0 === m[g] && delete m[g]; m = e.dataLabel = k[d ? "text" : "label"](r, 0, -9999, b.shape, null, null, b.useHTML, null, "data-label").attr(m); m.addClass("highcharts-data-label-color-" + e.colorIndex + " " + (b.className || "") + (b.useHTML ? "highcharts-tracker" : "")); m.css(h(x, A)); m.add(p); m.shadow(b.shadow)
                        } m && a.alignDataLabel(e, m, b, null, F)
                    }
                })
            }; c.prototype.alignDataLabel = function (a, c, b, d, f) {
                var m = this.chart, e = m.inverted,
                l = u(a.plotX, -9999), n = u(a.plotY, -9999), p = c.getBBox(), k, q = b.rotation, t = b.align, v = this.visible && (a.series.forceDL || m.isInsidePlot(l, Math.round(n), e) || d && m.isInsidePlot(l, e ? d.x + 1 : d.y + d.height - 1, e)), z = "justify" === u(b.overflow, "justify"); v && (k = b.style.fontSize, k = m.renderer.fontMetrics(k, c).b, d = h({ x: e ? m.plotWidth - n : l, y: Math.round(e ? m.plotHeight - l : n), width: 0, height: 0 }, d), h(b, { width: p.width, height: p.height }), q ? (z = !1, e = m.renderer.rotCorr(k, q), e = {
                    x: d.x + b.x + d.width / 2 + e.x, y: d.y + b.y + { top: 0, middle: .5, bottom: 1 }[b.verticalAlign] *
                        d.height
                }, c[f ? "attr" : "animate"](e).attr({ align: t }), l = (q + 720) % 360, l = 180 < l && 360 > l, "left" === t ? e.y -= l ? p.height : 0 : "center" === t ? (e.x -= p.width / 2, e.y -= p.height / 2) : "right" === t && (e.x -= p.width, e.y -= l ? 0 : p.height)) : (c.align(b, null, d), e = c.alignAttr), z ? this.justifyDataLabel(c, b, e, p, d, f) : u(b.crop, !0) && (v = m.isInsidePlot(e.x, e.y) && m.isInsidePlot(e.x + p.width, e.y + p.height)), b.shape && !q && c.attr({ anchorX: a.plotX, anchorY: a.plotY })); v || (c.attr({ y: -9999 }), c.placed = !1)
            }; c.prototype.justifyDataLabel = function (a, c, b, d, f, h) {
                var e =
                    this.chart, m = c.align, l = c.verticalAlign, n, k, p = a.box ? 0 : a.padding || 0; n = b.x + p; 0 > n && ("right" === m ? c.align = "left" : c.x = -n, k = !0); n = b.x + d.width - p; n > e.plotWidth && ("left" === m ? c.align = "right" : c.x = e.plotWidth - n, k = !0); n = b.y + p; 0 > n && ("bottom" === l ? c.verticalAlign = "top" : c.y = -n, k = !0); n = b.y + d.height - p; n > e.plotHeight && ("top" === l ? c.verticalAlign = "bottom" : c.y = e.plotHeight - n, k = !0); k && (a.placed = !h, a.align(c, null, f))
            }; n.pie && (n.pie.prototype.drawDataLabels = function () {
                var d = this, f = d.data, b, h = d.chart, l = d.options.dataLabels, n =
                    u(l.connectorPadding, 10), e = u(l.connectorWidth, 1), r = h.plotWidth, v = h.plotHeight, A, k = l.distance, w = d.center, y = w[2] / 2, D = w[1], G = 0 < k, g, B, L, M, R = [[], []], E, H, P, Q, O = [0, 0, 0, 0]; d.visible && (l.enabled || d._hasPointLabels) && (c.prototype.drawDataLabels.apply(d), I(f, function (a) { a.dataLabel && a.visible && (R[a.half].push(a), a.dataLabel._pos = null) }), I(R, function (c, e) {
                        var f, m, q = c.length, t, u, F; if (q) for (d.sortByAngle(c, e - .5), 0 < k && (f = Math.max(0, D - y - k), m = Math.min(D + y + k, h.plotHeight), t = p(c, function (a) {
                            if (a.dataLabel) return F =
                                a.dataLabel.getBBox().height || 21, { target: a.labelPos[1] - f + F / 2, size: F, rank: a.y }
                        }), a.distribute(t, m + F - f)), Q = 0; Q < q; Q++)b = c[Q], L = b.labelPos, g = b.dataLabel, P = !1 === b.visible ? "hidden" : "inherit", u = L[1], t ? void 0 === t[Q].pos ? P = "hidden" : (M = t[Q].size, H = f + t[Q].pos) : H = u, E = l.justify ? w[0] + (e ? -1 : 1) * (y + k) : d.getX(H < f + 2 || H > m - 2 ? u : H, e), g._attr = { visibility: P, align: L[6] }, g._pos = { x: E + l.x + ({ left: n, right: -n }[L[6]] || 0), y: H + l.y - 10 }, L.x = E, L.y = H, null === d.options.size && (B = g.width, E - B < n ? O[3] = Math.max(Math.round(B - E + n), O[3]) : E + B > r - n &&
                            (O[1] = Math.max(Math.round(E + B - r + n), O[1])), 0 > H - M / 2 ? O[0] = Math.max(Math.round(-H + M / 2), O[0]) : H + M / 2 > v && (O[2] = Math.max(Math.round(H + M / 2 - v), O[2])))
                    }), 0 === C(O) || this.verifyDataLabelOverflow(O)) && (this.placeDataLabels(), G && e && I(this.points, function (a) {
                        var b; A = a.connector; if ((g = a.dataLabel) && g._pos && a.visible) {
                            P = g._attr.visibility; if (b = !A) a.connector = A = h.renderer.path().addClass("highcharts-data-label-connector highcharts-color-" + a.colorIndex).add(d.dataLabelsGroup), A.attr({
                                "stroke-width": e, stroke: l.connectorColor ||
                                    a.color || "#666666"
                            }); A[b ? "attr" : "animate"]({ d: d.connectorPath(a.labelPos) }); A.attr("visibility", P)
                        } else A && (a.connector = A.destroy())
                    }))
            }, n.pie.prototype.connectorPath = function (a) { var c = a.x, b = a.y; return u(this.options.dataLabels.softConnector, !0) ? ["M", c + ("left" === a[6] ? 5 : -5), b, "C", c, b, 2 * a[2] - a[4], 2 * a[3] - a[5], a[2], a[3], "L", a[4], a[5]] : ["M", c + ("left" === a[6] ? 5 : -5), b, "L", a[2], a[3], "L", a[4], a[5]] }, n.pie.prototype.placeDataLabels = function () {
                I(this.points, function (a) {
                    var c = a.dataLabel; c && a.visible && ((a = c._pos) ?
                        (c.attr(c._attr), c[c.moved ? "animate" : "attr"](a), c.moved = !0) : c && c.attr({ y: -9999 }))
                })
            }, n.pie.prototype.alignDataLabel = l, n.pie.prototype.verifyDataLabelOverflow = function (a) {
                var c = this.center, b = this.options, f = b.center, h = b.minSize || 80, l, e; null !== f[0] ? l = Math.max(c[2] - Math.max(a[1], a[3]), h) : (l = Math.max(c[2] - a[1] - a[3], h), c[0] += (a[3] - a[1]) / 2); null !== f[1] ? l = Math.max(Math.min(l, c[2] - Math.max(a[0], a[2])), h) : (l = Math.max(Math.min(l, c[2] - a[0] - a[2]), h), c[1] += (a[0] - a[2]) / 2); l < c[2] ? (c[2] = l, c[3] = Math.min(d(b.innerSize ||
                    0, l), l), this.translate(c), this.drawDataLabels && this.drawDataLabels()) : e = !0; return e
            }); n.column && (n.column.prototype.alignDataLabel = function (a, d, b, f, h) {
                var l = this.chart.inverted, e = a.series, m = a.dlBox || a.shapeArgs, n = u(a.below, a.plotY > u(this.translatedThreshold, e.yAxis.len)), p = u(b.inside, !!this.options.stacking); m && (f = v(m), 0 > f.y && (f.height += f.y, f.y = 0), m = f.y + f.height - e.yAxis.len, 0 < m && (f.height -= m), l && (f = { x: e.yAxis.len - f.y - f.height, y: e.xAxis.len - f.x - f.width, width: f.height, height: f.width }), p || (l ? (f.x += n ?
                    0 : f.width, f.width = 0) : (f.y += n ? f.height : 0, f.height = 0))); b.align = u(b.align, !l || p ? "center" : n ? "right" : "left"); b.verticalAlign = u(b.verticalAlign, l || p ? "middle" : n ? "top" : "bottom"); c.prototype.alignDataLabel.call(this, a, d, b, f, h)
            })
    })(L); (function (a) {
        var D = a.Chart, C = a.each, G = a.pick, I = a.addEvent; D.prototype.callbacks.push(function (a) {
            function f() {
                var f = []; C(a.series, function (a) {
                    var h = a.options.dataLabels, p = a.dataLabelCollections || ["dataLabel"]; (h.enabled || a._hasPointLabels) && !h.allowOverlap && a.visible && C(p, function (d) {
                        C(a.points,
                            function (a) { a[d] && (a[d].labelrank = G(a.labelrank, a.shapeArgs && a.shapeArgs.height), f.push(a[d])) })
                    })
                }); a.hideOverlappingLabels(f)
            } f(); I(a, "redraw", f)
        }); D.prototype.hideOverlappingLabels = function (a) {
            var f = a.length, h, v, l, u, d, c, n, y, t, m = function (a, c, d, f, e, h, l, m) { return !(e > a + d || e + l < a || h > c + f || h + m < c) }; for (v = 0; v < f; v++)if (h = a[v]) h.oldOpacity = h.opacity, h.newOpacity = 1; a.sort(function (a, c) { return (c.labelrank || 0) - (a.labelrank || 0) }); for (v = 0; v < f; v++)for (l = a[v], h = v + 1; h < f; ++h)if (u = a[h], l && u && l.placed && u.placed && 0 !==
                l.newOpacity && 0 !== u.newOpacity && (d = l.alignAttr, c = u.alignAttr, n = l.parentGroup, y = u.parentGroup, t = 2 * (l.box ? 0 : l.padding), d = m(d.x + n.translateX, d.y + n.translateY, l.width - t, l.height - t, c.x + y.translateX, c.y + y.translateY, u.width - t, u.height - t))) (l.labelrank < u.labelrank ? l : u).newOpacity = 0; C(a, function (a) { var b, c; a && (c = a.newOpacity, a.oldOpacity !== c && a.placed && (c ? a.show(!0) : b = function () { a.hide() }, a.alignAttr.opacity = c, a[a.isOld ? "animate" : "attr"](a.alignAttr, null, b)), a.isOld = !0) })
        }
    })(L); (function (a) {
        var D = a.addEvent,
        C = a.Chart, G = a.createElement, I = a.css, h = a.defaultOptions, f = a.defaultPlotOptions, p = a.each, v = a.extend, l = a.fireEvent, u = a.hasTouch, d = a.inArray, c = a.isObject, n = a.Legend, y = a.merge, t = a.pick, m = a.Point, b = a.Series, q = a.seriesTypes, z = a.svg; a = a.TrackerMixin = {
            drawTrackerPoint: function () {
                var a = this, b = a.chart, c = b.pointer, d = function (a) { for (var c = a.target, e; c && !e;)e = c.point, c = c.parentNode; if (void 0 !== e && e !== b.hoverPoint) e.onMouseOver(a) }; p(a.points, function (a) {
                a.graphic && (a.graphic.element.point = a); a.dataLabel && (a.dataLabel.div ?
                    a.dataLabel.div.point = a : a.dataLabel.element.point = a)
                }); a._hasTracking || (p(a.trackerGroups, function (b) { if (a[b]) { a[b].addClass("highcharts-tracker").on("mouseover", d).on("mouseout", function (a) { c.onTrackerMouseOut(a) }); if (u) a[b].on("touchstart", d); a.options.cursor && a[b].css(I).css({ cursor: a.options.cursor }) } }), a._hasTracking = !0)
            }, drawTrackerGraph: function () {
                var a = this, b = a.options, c = b.trackByArea, d = [].concat(c ? a.areaPath : a.graphPath), f = d.length, h = a.chart, l = h.pointer, m = h.renderer, n = h.options.tooltip.snap,
                q = a.tracker, g, t = function () { if (h.hoverSeries !== a) a.onMouseOver() }, v = "rgba(192,192,192," + (z ? .0001 : .002) + ")"; if (f && !c) for (g = f + 1; g--;)"M" === d[g] && d.splice(g + 1, 0, d[g + 1] - n, d[g + 2], "L"), (g && "M" === d[g] || g === f) && d.splice(g, 0, "L", d[g - 2] + n, d[g - 1]); q ? q.attr({ d: d }) : a.graph && (a.tracker = m.path(d).attr({ "stroke-linejoin": "round", visibility: a.visible ? "visible" : "hidden", stroke: v, fill: c ? v : "none", "stroke-width": a.graph.strokeWidth() + (c ? 0 : 2 * n), zIndex: 2 }).add(a.group), p([a.tracker, a.markerGroup], function (a) {
                    a.addClass("highcharts-tracker").on("mouseover",
                        t).on("mouseout", function (a) { l.onTrackerMouseOut(a) }); b.cursor && a.css({ cursor: b.cursor }); if (u) a.on("touchstart", t)
                }))
            }
        }; q.column && (q.column.prototype.drawTracker = a.drawTrackerPoint); q.pie && (q.pie.prototype.drawTracker = a.drawTrackerPoint); q.scatter && (q.scatter.prototype.drawTracker = a.drawTrackerPoint); v(n.prototype, {
            setItemEvents: function (a, b, c) {
                var e = this, d = e.chart, f = "highcharts-legend-" + (a.series ? "point" : "series") + "-active"; (c ? b : a.legendGroup).on("mouseover", function () {
                    a.setState("hover"); d.seriesGroup.addClass(f);
                    b.css(e.options.itemHoverStyle)
                }).on("mouseout", function () { b.css(a.visible ? e.itemStyle : e.itemHiddenStyle); d.seriesGroup.removeClass(f); a.setState() }).on("click", function (b) { var c = function () { a.setVisible && a.setVisible() }; b = { browserEvent: b }; a.firePointEvent ? a.firePointEvent("legendItemClick", b, c) : l(a, "legendItemClick", b, c) })
            }, createCheckboxForItem: function (a) {
            a.checkbox = G("input", { type: "checkbox", checked: a.selected, defaultChecked: a.selected }, this.options.itemCheckboxStyle, this.chart.container); D(a.checkbox,
                "click", function (b) { l(a.series || a, "checkboxClick", { checked: b.target.checked, item: a }, function () { a.select() }) })
            }
        }); h.legend.itemStyle.cursor = "pointer"; v(C.prototype, {
            showResetZoom: function () {
                var a = this, b = h.lang, c = a.options.chart.resetZoomButton, d = c.theme, f = d.states, k = "chart" === c.relativeTo ? null : "plotBox"; this.resetZoomButton = a.renderer.button(b.resetZoom, null, null, function () { a.zoomOut() }, d, f && f.hover).attr({ align: c.position.align, title: b.resetZoomTitle }).addClass("highcharts-reset-zoom").add().align(c.position,
                    !1, k)
            }, zoomOut: function () { var a = this; l(a, "selection", { resetSelection: !0 }, function () { a.zoom() }) }, zoom: function (a) { var b, d = this.pointer, f = !1, h; !a || a.resetSelection ? p(this.axes, function (a) { b = a.zoom() }) : p(a.xAxis.concat(a.yAxis), function (a) { var c = a.axis; d[c.isXAxis ? "zoomX" : "zoomY"] && (b = c.zoom(a.min, a.max), c.displayBtn && (f = !0)) }); h = this.resetZoomButton; f && !h ? this.showResetZoom() : !f && c(h) && (this.resetZoomButton = h.destroy()); b && this.redraw(t(this.options.chart.animation, a && a.animation, 100 > this.pointCount)) },
            pan: function (a, b) {
                var c = this, e = c.hoverPoints, d; e && p(e, function (a) { a.setState() }); p("xy" === b ? [1, 0] : [1], function (b) { b = c[b ? "xAxis" : "yAxis"][0]; var e = b.horiz, f = a[e ? "chartX" : "chartY"], e = e ? "mouseDownX" : "mouseDownY", h = c[e], k = (b.pointRange || 0) / 2, g = b.getExtremes(), l = b.toValue(h - f, !0) + k, k = b.toValue(h + b.len - f, !0) - k, m = k < l, h = m ? k : l, l = m ? l : k, k = Math.min(g.dataMin, g.min) - h, g = l - Math.max(g.dataMax, g.max); b.series.length && 0 > k && 0 > g && (b.setExtremes(h, l, !1, !1, { trigger: "pan" }), d = !0); c[e] = f }); d && c.redraw(!1); I(c.container,
                    { cursor: "move" })
            }
        }); v(m.prototype, {
            select: function (a, b) { var c = this, e = c.series, f = e.chart; a = t(a, !c.selected); c.firePointEvent(a ? "select" : "unselect", { accumulate: b }, function () { c.selected = c.options.selected = a; e.options.data[d(c, e.data)] = c.options; c.setState(a && "select"); b || p(f.getSelectedPoints(), function (a) { a.selected && a !== c && (a.selected = a.options.selected = !1, e.options.data[d(a, e.data)] = a.options, a.setState(""), a.firePointEvent("unselect")) }) }) }, onMouseOver: function (a, b) {
                var c = this.series, e = c.chart, d =
                    e.tooltip, f = e.hoverPoint; if (this.series) { if (!b) { if (f && f !== this) f.onMouseOut(); if (e.hoverSeries !== c) c.onMouseOver(); e.hoverPoint = this } !d || d.shared && !c.noSharedTooltip ? d || this.setState("hover") : (this.setState("hover"), d.refresh(this, a)); this.firePointEvent("mouseOver") }
            }, onMouseOut: function () { var a = this.series.chart, b = a.hoverPoints; this.firePointEvent("mouseOut"); b && -1 !== d(this, b) || (this.setState(), a.hoverPoint = null) }, importEvents: function () {
                if (!this.hasImportedEvents) {
                    var a = y(this.series.options.point,
                        this.options).events, b; this.events = a; for (b in a) D(this, b, a[b]); this.hasImportedEvents = !0
                }
            }, setState: function (a, b) {
                var c = Math.floor(this.plotX), d = this.plotY, e = this.series, h = e.options.states[a] || {}, l = f[e.type].marker && e.options.marker, m = l && !1 === l.enabled, n = l && l.states && l.states[a] || {}, p = !1 === n.enabled, g = e.stateMarkerGraphic, q = this.marker || {}, u = e.chart, y = e.halo, z, F = l && e.markerAttribs; a = a || ""; if (!(a === this.state && !b || this.selected && "select" !== a || !1 === h.enabled || a && (p || m && !1 === n.enabled) || a && q.states &&
                    q.states[a] && !1 === q.states[a].enabled)) {
                        F && (z = e.markerAttribs(this, a)); if (this.graphic) this.state && this.graphic.removeClass("highcharts-point-" + this.state), a && this.graphic.addClass("highcharts-point-" + a), this.graphic.attr(e.pointAttribs(this, a)), z && this.graphic.animate(z, t(u.options.chart.animation, n.animation, l.animation)), g && g.hide(); else {
                            if (a && n) {
                                l = q.symbol || e.symbol; g && g.currentSymbol !== l && (g = g.destroy()); if (g) g[b ? "animate" : "attr"]({ x: z.x, y: z.y }); else l && (e.stateMarkerGraphic = g = u.renderer.symbol(l,
                                    z.x, z.y, z.width, z.height).add(e.markerGroup), g.currentSymbol = l); g && g.attr(e.pointAttribs(this, a))
                            } g && (g[a && u.isInsidePlot(c, d, u.inverted) ? "show" : "hide"](), g.element.point = this)
                        } (c = h.halo) && c.size ? (y || (e.halo = y = u.renderer.path().add(F ? e.markerGroup : e.group)), y[b ? "animate" : "attr"]({ d: this.haloPath(c.size) }), y.attr({ "class": "highcharts-halo highcharts-color-" + t(this.colorIndex, e.colorIndex) }), y.point = this, y.attr(v({ fill: this.color || e.color, "fill-opacity": c.opacity, zIndex: -1 }, c.attributes))) : y && y.point &&
                            y.point.haloPath && y.animate({ d: y.point.haloPath(0) }); this.state = a
                }
            }, haloPath: function (a) { return this.series.chart.renderer.symbols.circle(Math.floor(this.plotX) - a, this.plotY - a, 2 * a, 2 * a) }
        }); v(b.prototype, {
            onMouseOver: function () { var a = this.chart, b = a.hoverSeries; if (b && b !== this) b.onMouseOut(); this.options.events.mouseOver && l(this, "mouseOver"); this.setState("hover"); a.hoverSeries = this }, onMouseOut: function () {
                var a = this.options, b = this.chart, c = b.tooltip, d = b.hoverPoint; b.hoverSeries = null; if (d) d.onMouseOut();
                this && a.events.mouseOut && l(this, "mouseOut"); !c || a.stickyTracking || c.shared && !this.noSharedTooltip || c.hide(); this.setState()
            }, setState: function (a) {
                var b = this, c = b.options, d = b.graph, f = c.states, h = c.lineWidth, c = 0; a = a || ""; if (b.state !== a && (p([b.group, b.markerGroup], function (c) { c && (b.state && c.removeClass("highcharts-series-" + b.state), a && c.addClass("highcharts-series-" + a)) }), b.state = a, !f[a] || !1 !== f[a].enabled) && (a && (h = f[a].lineWidth || h + (f[a].lineWidthPlus || 0)), d && !d.dashstyle)) for (f = { "stroke-width": h }, d.attr(f); b["zone-graph-" +
                    c];)b["zone-graph-" + c].attr(f), c += 1
            }, setVisible: function (a, b) {
                var c = this, d = c.chart, e = c.legendItem, f, h = d.options.chart.ignoreHiddenSeries, m = c.visible; f = (c.visible = a = c.options.visible = c.userOptions.visible = void 0 === a ? !m : a) ? "show" : "hide"; p(["group", "dataLabelsGroup", "markerGroup", "tracker", "tt"], function (a) { if (c[a]) c[a][f]() }); if (d.hoverSeries === c || (d.hoverPoint && d.hoverPoint.series) === c) c.onMouseOut(); e && d.legend.colorizeItem(c, a); c.isDirty = !0; c.options.stacking && p(d.series, function (a) {
                    a.options.stacking &&
                    a.visible && (a.isDirty = !0)
                }); p(c.linkedSeries, function (b) { b.setVisible(a, !1) }); h && (d.isDirtyBox = !0); !1 !== b && d.redraw(); l(c, f)
            }, show: function () { this.setVisible(!0) }, hide: function () { this.setVisible(!1) }, select: function (a) { this.selected = a = void 0 === a ? !this.selected : a; this.checkbox && (this.checkbox.checked = a); l(this, a ? "select" : "unselect") }, drawTracker: a.drawTrackerGraph
        })
    })(L); (function (a) {
        var D = a.Chart, C = a.each, G = a.inArray, I = a.isObject, h = a.pick, f = a.splat; D.prototype.setResponsive = function (a) {
            var f = this.options.responsive;
            f && f.rules && C(f.rules, function (f) { this.matchResponsiveRule(f, a) }, this)
        }; D.prototype.matchResponsiveRule = function (f, v) {
            var l = this.respRules, p = f.condition, d; d = p.callback || function () { return this.chartWidth <= h(p.maxWidth, Number.MAX_VALUE) && this.chartHeight <= h(p.maxHeight, Number.MAX_VALUE) && this.chartWidth >= h(p.minWidth, 0) && this.chartHeight >= h(p.minHeight, 0) }; void 0 === f._id && (f._id = a.uniqueKey()); d = d.call(this); !l[f._id] && d ? f.chartOptions && (l[f._id] = this.currentOptions(f.chartOptions), this.update(f.chartOptions,
                v)) : l[f._id] && !d && (this.update(l[f._id], v), delete l[f._id])
        }; D.prototype.currentOptions = function (a) { function h(a, d, c) { var l, p; for (l in a) if (-1 < G(l, ["series", "xAxis", "yAxis"])) for (a[l] = f(a[l]), c[l] = [], p = 0; p < a[l].length; p++)c[l][p] = {}, h(a[l][p], d[l][p], c[l][p]); else I(a[l]) ? (c[l] = {}, h(a[l], d[l] || {}, c[l])) : c[l] = d[l] || null } var l = {}; h(a, this.options, l); return l }
    })(L); return L
});
