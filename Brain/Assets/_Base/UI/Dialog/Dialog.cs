using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace BaseFramework.UI {
    [RequireComponent(typeof(Image))]
    public abstract class Dialog : MonoBehaviour, IPointerDownHandler {
        public enum DialogStatus {
            None,
            Init,
            Opening,
            Opened,
            Closing,
            Disable,
            Closed
        }

        public DialogStatus dialogStatus;
        public bool ignoreRaycaster = false;
        public bool dontDestroyOnLoad = false;
        public bool destroyOnClose = true;
        public bool closeByBack = true;
        public bool closeByClickBlank = false;
        public bool playOpenSound = true;
        public bool playCloseSound = true;
        public bool playOpenAnimation = true;
        public bool playCloseAnimation = false;
        public float openAnimationTime = 0.5f;
        public float closeAnimationTime = 0.25f;

        public string dialogName { get; private set; }

        protected RectTransform rectTrans;
        protected RectTransform fit;
        protected RectTransform content;
        protected Image bgImage;
        protected GraphicRaycaster graphicRaycaster;

        private event Action<Dialog> _onDialogOpened;
        private event Action<Dialog> _onDialogClose;
        private event Action<Dialog> _onDialogDisable;
        private event Action<Dialog> _onDialogDestroy;

        private event Action _onOpened;
        private event Action _onClose;
        private event Action _onDisable;
        private event Action _onDestroy;


        protected virtual void Awake() {
            dialogStatus = DialogStatus.None;

            graphicRaycaster = GetComponent<GraphicRaycaster>();
            bgImage = GetComponent<Image>();
            fit = transform.Find("Fit") as RectTransform;
            content = transform.Find("Content") as RectTransform;
            if (content == null) {
                content = transform.Find("Fit/Content") as RectTransform;
            }

            transform.SetAsLastSibling();

            SetIgnoreRaycaster(ignoreRaycaster);

            if (dontDestroyOnLoad) {
                DontDestroyOnLoad(gameObject);
            }

            dialogStatus = DialogStatus.Init;
        }

        protected virtual void OnEnable() {
            dialogStatus = DialogStatus.Opening;

            if (playOpenSound) {
                PlayOpenSound();
            }

            if (playOpenAnimation && content != null) {
                PlayOpenAnimation();
            } else {
                _onOpened?.Invoke();
                _onDialogOpened?.Invoke(this);

                dialogStatus = DialogStatus.Opened;
            }
        }

        private void Update() {
            if (closeByBack && Input.GetKeyDown(KeyCode.Escape)) {
                OnBackClick();
            }
        }

        protected virtual void OnBackClick() {
            Close();
        }

        protected virtual void OnDisable() {
            dialogStatus = DialogStatus.Disable;

            _onDisable?.Invoke();
            _onDialogDisable?.Invoke(this);

            _onDialogOpened = null;
            _onDialogClose = null;
            _onDialogDisable = null;

            _onOpened = null;
            _onClose = null;
            _onDisable = null;
        }

        protected virtual void OnDestroy() {
            dialogStatus = DialogStatus.None;

            _onDestroy?.Invoke();
            _onDialogDestroy?.Invoke(this);
            _onDialogDestroy = null;
            _onDestroy = null;
        }

        public virtual Dialog SetDialogName(string name) {
            this.dialogName = name;

            return this;
        }

        public virtual Dialog SetIgnoreRaycaster(bool ignoreRaycaster) {
            this.ignoreRaycaster = ignoreRaycaster;
            graphicRaycaster.enabled = !ignoreRaycaster;

            return this;
        }

        protected virtual void PlayOpenSound() {
        }

        protected virtual void PlayCloseSound() {
        }

        private void PlayOpenAnimation() {
            Tweener anim = GetOpenAnimation();
            if (anim == null) {
                _onOpened?.Invoke();
                _onDialogOpened?.Invoke(this);
                dialogStatus = DialogStatus.Opened;
            } else {
                anim.OnComplete(() => {
                    _onOpened?.Invoke();
                    _onDialogOpened?.Invoke(this);

                    dialogStatus = DialogStatus.Opened;
                });
            }
        }

        protected virtual Tweener GetOpenAnimation() {
            return null;
        }

        private void PlayCloseAnimation() {
            Tweener anim = GetCloseAnimation();
            if (anim == null) {
                DoClose();
            } else {
                anim.OnComplete(DoClose);
            }
        }

        protected virtual Tweener GetCloseAnimation() {
            return null;
        }

        public virtual void OnPointerDown(PointerEventData eventData) {
            if (closeByClickBlank && eventData.pointerEnter == gameObject) {
                Close();
            }
        }

        public virtual Dialog Open() {
            if (!IsOpened()) {
                gameObject.SetActive(true);
            }

            return this;
        }

        public virtual bool IsOpened() {
            if (this == null || gameObject == null) {
                return false;
            }

            return dialogStatus == DialogStatus.Opening && gameObject.activeSelf;
        }

        public virtual void Close() {
            if (this == null || gameObject == null) {
                return;
            }

            if (dialogStatus != DialogStatus.Opened) return;
            dialogStatus = DialogStatus.Closing;

            _onClose?.Invoke();
            _onDialogClose?.Invoke(this);

            if (playCloseSound) {
                PlayCloseSound();
            }

            if (playCloseAnimation) {
                PlayCloseAnimation();
            } else {
                DoClose();
            }
        }

        private void DoClose() {
            if (destroyOnClose) {
                gameObject.Destroy();
            } else {
                gameObject.Inactive();
            }

            dialogStatus = DialogStatus.Closed;
        }

        public Dialog OnOpen(Action action) {
            _onOpened += action;

            return this;
        }

        public Dialog OnOpend(Action<Dialog> action) {
            _onDialogOpened += action;

            return this;
        }

        public Dialog OnClose(Action action) {
            _onClose += action;

            return this;
        }

        public Dialog OnClose(Action<Dialog> action) {
            _onDialogClose += action;

            return this;
        }

        public Dialog OnDisable(Action action) {
            _onDisable += action;

            return this;
        }

        public Dialog OnDisable(Action<Dialog> action) {
            _onDialogDisable += action;

            return this;
        }

        public Dialog OnDestroy(Action action) {
            _onDestroy += action;

            return this;
        }

        public Dialog OnDestroy(Action<Dialog> action) {
            _onDialogDestroy += action;

            return this;
        }
    }
}
