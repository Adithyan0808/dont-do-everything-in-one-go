function RememberMeCheckbox({ register }) {
  return (
    <label className="inline-flex items-center gap-2 text-sm text-slate-700">
      <input
        type="checkbox"
        className="h-4 w-4 rounded border-slate-300 text-primary focus:ring-primary"
        {...register('rememberMe')}
      />
      Remember me
    </label>
  );
}

export default RememberMeCheckbox;
