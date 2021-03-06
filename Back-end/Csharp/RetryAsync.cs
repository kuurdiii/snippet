//Call
var result = await Retry(async () => await MyMethodAsync(), TimeSpan.FromSeconds(10), 5);

private async Task<T> Retry<T>(Func<Task<T>> action, TimeSpan retryInterval, int maxAttemptCount = 3)
{
	var exceptions = new List<Exception>();

	for (int attempted = 0; attempted < maxAttemptCount; attempted++)
	{
		try
		{
			if (attempted > 0)
				await Task.Delay(retryInterval);

			return await action();
		}
		catch (Exception ex)
		{
			exceptions.Add(ex);
		}
	}

	throw new AggregateException(exceptions);
}
